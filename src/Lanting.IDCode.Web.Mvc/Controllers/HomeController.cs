using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Lanting.IDCode.Controllers;
using Lanting.IDCode.Sessions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Abp.Domain.Repositories;
using Lanting.IDCode.Entity;
using Lanting.IDCode.Core.IRepositories;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Lanting.IDCode.Application;

namespace Lanting.IDCode.Web.Controllers
{
    public class HomeController : IDCodeControllerBase
    {
        private readonly IConfiguration _configuration;
        private string _defaultUrl { get; set; }
        private readonly IIDentityCodeRepository _identityCodeRepository;
        private readonly IRepository<ProductInfo> _productRepository;
        private readonly IRepository<Authorization.Users.User, long> _userRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ISessionAppService _sessionAppService;

        public HomeController(IConfiguration configuration, IIDentityCodeRepository identityCodeRepository, IRepository<ProductInfo> productRepository, IRepository<Authorization.Users.User, long> userRepository, IHostingEnvironment hostingEnvironment, ISessionAppService sessionAppService)
        {
            _configuration = configuration;
            _defaultUrl = _configuration.GetSection("DefaultUrl").Value;
            _identityCodeRepository = identityCodeRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _hostingEnvironment = hostingEnvironment;
            _sessionAppService = sessionAppService;
        }
        public async Task<IActionResult> Index(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Redirect("home.html");
            }
            //get the index
            string indexStr = code.Substring(6, 8);

            long index = long.Parse(indexStr);

            //get the code data form db
            var codeRecord = await _identityCodeRepository.GetAsync(index);
            if (codeRecord == null)
                throw new Abp.UI.UserFriendlyException(404, "invalid code");
            if (!code.Equals(codeRecord.Code))
                throw new Abp.UI.UserFriendlyException(404, "invalid code");

            //get the product
            var product = await _productRepository.GetAsync(codeRecord.ProductId);
            var user = await _userRepository.GetAsync(product.UserId);
            string username = user.UserName;
            string productcode = product.Code;
            string htmlPath =  $"{_hostingEnvironment.WebRootPath}\\codepage\\{username}\\{PageType.mobile.ToString()}\\{productcode}.html";
            return new Commons.HtmlFileResult(htmlPath, "text/html", codeRecord.AntiFakeCode);
        }

        public async Task<IActionResult> Label(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Redirect("home.html");
            }
            //get the index
            string indexStr = code.Substring(6, 8);

            long index = long.Parse(indexStr);

            //get the code data form db
            var codeRecord = await _identityCodeRepository.GetAsync(index);
            if (codeRecord == null)
                throw new Abp.UI.UserFriendlyException(404, "invalid code");
            if (!code.Equals(codeRecord.Code))
                throw new Abp.UI.UserFriendlyException(404, "invalid code");

            //get the product
            var product = await _productRepository.GetAsync(codeRecord.ProductId);
            var user = await _userRepository.GetAsync(product.UserId);
            string username = user.UserName;
            string productcode = product.Code;
            string codeUrl = _configuration.GetSection("DefaultUrl").Value + code;
            string imageFilePath =  $"{_hostingEnvironment.WebRootPath}\\codepage\\{username}\\{PageType.label.ToString()}\\qr_code_temp.gif";
            CodeHelper.CreateCode(codeUrl, imageFilePath);
            string htmlPath = $"{_hostingEnvironment.WebRootPath}\\codepage/{username}\\{PageType.label.ToString()}\\{productcode}.html";
            return new Commons.HtmlFileResult(htmlPath, "text/html", codeRecord.AntiFakeCode);
        }

        public async Task<IActionResult> CodePreview(string productCode)
        {
            var currentUser = await _sessionAppService.GetCurrentLoginInformations();
            // ./codepage/nne/mobile/xxxx.html
            string htmlFilePath = $"{_hostingEnvironment.WebRootPath}/codepage/{currentUser.User.UserName}/{PageType.mobile.ToString()}/{productCode}.html";
            return new Commons.HtmlFileResult(htmlFilePath, "text/html");
        }
    }
}
