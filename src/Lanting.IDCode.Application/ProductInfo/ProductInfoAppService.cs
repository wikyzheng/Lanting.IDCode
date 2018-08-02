using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Authorization;
using Lanting.IDCode.Entity;
using Lanting.IDCode.Authorization;
using Lanting.IDCode.Sessions;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Castle.Core.Logging;
using System.Text;
using Abp.UI;
using Lanting.IDCode.Users;
using Lanting.IDCode.Authorization.Users;

namespace Lanting.IDCode.Application
{
    [AbpAuthorize(PermissionNames.Pages_Codes)]
    public partial class ProductInfoAppService : AsyncCrudAppService<ProductInfo, ProductInfoDto, int, PagedResultRequestDto, CreateProductInfoDto, ProductInfoDto>, IProductInfoAppService
    {
        /// <summary>
        /// ProductInfoRepository
        /// </summary>
        private readonly IRepository<ProductInfo, int> _productInfoRepository;
        private readonly ISessionAppService _sessionAppService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _htmlDiretory = "codepage";
        private readonly string _qrGenerateApi = "http://bshare.optimix.asia/barCode?site=weixin&url=";
        private string _defaultUrl { get; set; }
        private readonly IConfiguration _configuration;
        private ILogger _logger { get; set; }
        private readonly IRepository<User, long> _userRepository;
        /// <summary>
        /// EventBus
        /// </summary>
        public IEventBus EventBus { get; set; }

        public ProductInfoAppService(IRepository<ProductInfo, int> productInfoRepository, ISessionAppService sessionAppService, IHostingEnvironment hostingEnvironment, IConfiguration configuration, IRepository<User, long> userRepository) : base(productInfoRepository)
        {
            _productInfoRepository = productInfoRepository;
            _sessionAppService = sessionAppService;
            _hostingEnvironment = hostingEnvironment;
            EventBus = NullEventBus.Instance;
            _configuration = configuration;
            _defaultUrl = _configuration.GetSection("DefaultUrl").Value;
            _logger = NullLogger.Instance;
            _userRepository = userRepository;
        }

        public override async Task<ProductInfoDto> Create(CreateProductInfoDto input)
        {
            int currentUserId = (int)(base.AbpSession.UserId ?? 0);

            var dupliatedOne = await _productInfoRepository.FirstOrDefaultAsync(x => (x.Code.Equals(input.Code, StringComparison.OrdinalIgnoreCase) || x.FullName.Equals(input.FullName, StringComparison.OrdinalIgnoreCase)) && x.UserId == currentUserId);

            if (dupliatedOne != null)
                throw new UserFriendlyException("名称有重复！");

            var currentUser = await _userRepository.GetAsync(currentUserId);

            int productCount = await _productInfoRepository.CountAsync(x => x.UserId == currentUserId);

            if (productCount + 1 > currentUser.AllowProductCount)
                throw new UserFriendlyException($"当前用户只允许创建{currentUser.AllowProductCount}个页面，请联系管理员！");

            var productInfo = ObjectMapper.Map<ProductInfo>(input);

            productInfo.Created = DateTime.Now;
            productInfo.IsEnabled = true;
            productInfo.UserId = currentUserId;

            var entity = await _productInfoRepository.InsertAsync(productInfo);
            var dto = ObjectMapper.Map<ProductInfoDto>(entity);
            if (!string.IsNullOrEmpty(input.HtmlContent))
                await GenerateHtml(input.Code, input.HtmlContent);
            return await Task.FromResult(dto);

        }

        public override async Task<ProductInfoDto> Get(EntityDto<int> input)
        {
            var entity = _productInfoRepository.Get(input.Id);
            var dto = ObjectMapper.Map<ProductInfoDto>(entity);
            dto.HtmlContent = await GetHtmlContent(dto.Code);
            return await Task.FromResult<ProductInfoDto>(dto);
        }

        public override async Task<ProductInfoDto> Update(ProductInfoDto input)
        {
            var productInfo = await _productInfoRepository.SingleAsync(x => x.Id == input.Id);
            productInfo.FullName = input.FullName;
            productInfo.Description = input.Description;
            productInfo.Modified = DateTime.Now;
            if (!string.IsNullOrEmpty(input.HtmlContent))
                await GenerateHtml(input.Code, input.HtmlContent);
            return ObjectMapper.Map<ProductInfoDto>(productInfo);
        }

        public override async Task<PagedResultDto<ProductInfoDto>> GetAll(PagedResultRequestDto input)
        {
            var user = await _sessionAppService.GetCurrentLoginInformations();

            var all = from x in _productInfoRepository.GetAll().Where(x => x.UserId == user.User.Id)
                      select ObjectMapper.Map<ProductInfoDto>(x);

            var pagedResultDto = new PagedResultDto<ProductInfoDto>();
            pagedResultDto.Items = all.ToList().AsReadOnly();

            foreach (var item in pagedResultDto.Items)
            {
                item.QRCodeImage = $"{_defaultUrl}images/{user.User.UserName}/{item.Code}.png";
            }

            pagedResultDto.TotalCount = all.Count();

            return await Task.FromResult(pagedResultDto);
        }

        public async Task GenerateHtml(string productCode, string htmlContent)
        {
            var user = await _sessionAppService.GetCurrentLoginInformations();
            var fileName = $"{productCode}.html";
            //  root/codepage/baolong/dd.html
            string dir = Path.Combine(_hostingEnvironment.WebRootPath, _htmlDiretory, user.User.UserName);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            string filePath = Path.Combine(dir, fileName);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            sb.AppendLine("<link href=\"../codepage.css\" rel=\"stylesheet\" />");
            sb.Append(htmlContent);
            File.WriteAllText(filePath, sb.ToString(), System.Text.Encoding.UTF8);

            //即时生成二维码图片
            string imageDir = Path.Combine(_hostingEnvironment.WebRootPath, "images", user.User.UserName);

            _logger.Info($"image path is {imageDir}");

            if (!Directory.Exists(imageDir))
                Directory.CreateDirectory(imageDir);
            string imageName = $"{productCode}.png";
            // string imagePath = Path.Combine(imageDir, imageName);
            string qrContent = $"{_defaultUrl}codepage/{user.User.UserName}/{fileName}";

            _logger.Info($"qr content is {qrContent}");

            await GetQrImage(qrContent, _qrGenerateApi, imageDir, imageName);

        }

        public async Task<string> GetHtmlContent(string productCode)
        {
            var user = await _sessionAppService.GetCurrentLoginInformations();
            var fileName = $"{productCode}.html";
            //  root/codepage/baolong/dd.html
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, _htmlDiretory, user.User.UserName, fileName);
            if (!File.Exists(filePath))
                return string.Empty;
            return await File.ReadAllTextAsync(filePath);
        }

        public async Task<string> GetQrImage(string genearteUrl, string generateApi, string savePath, string fileName)
        {
            string imagePath = Path.Combine(savePath, fileName);
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            HttpClient hc = new HttpClient();
            var result = hc.GetStreamAsync(generateApi + genearteUrl).Result;
            string fullFilePath = System.IO.Path.Combine(savePath, fileName);
            using (FileStream fs = File.Create(fullFilePath))
            {
                await result.CopyToAsync(fs);
                await fs.FlushAsync();
            }
            return fullFilePath;
        }
    }
}