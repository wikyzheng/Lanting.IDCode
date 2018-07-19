using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Lanting.IDCode.Controllers;
using Lanting.IDCode.Sessions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Abp.Domain.Repositories;
using Lanting.IDCode.Entity;
using Lanting.IDCode.Core.IRepositories;

namespace Lanting.IDCode.Web.Controllers
{
    public class HomeController : IDCodeControllerBase
    {
        private readonly IConfiguration _configuration;
        private string _defaultUrl { get; set; }
        private readonly IIDentityCodeRepository _identityCodeRepository;
        private readonly IRepository<ProductInfo> _productRepository;
        private readonly IRepository<Authorization.Users.User, long> _userRepository;
        public HomeController(IConfiguration configuration, IIDentityCodeRepository identityCodeRepository, IRepository<ProductInfo> productRepository, IRepository<Authorization.Users.User, long> userRepository)
        {
            _configuration = configuration;
            _defaultUrl = _configuration.GetSection("DefaultUrl").Value;
            _identityCodeRepository = identityCodeRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }
        public async Task<ActionResult> Index(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return RedirectToAction("Index", "Task");
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
            //get the html url
            return Redirect($"{_defaultUrl}/codepage/{username}/{productcode}.html");
        }
    }
}
