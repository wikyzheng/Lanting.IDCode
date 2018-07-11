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
        /// <summary>
        /// EventBus
        /// </summary>
        public IEventBus EventBus { get; set; }

        public ProductInfoAppService(IRepository<ProductInfo, int> productInfoRepository, ISessionAppService sessionAppService, IHostingEnvironment hostingEnvironment) : base(productInfoRepository)
        {
            _productInfoRepository = productInfoRepository;
            _sessionAppService = sessionAppService;
            _hostingEnvironment = hostingEnvironment;
            EventBus = NullEventBus.Instance;
        }

        public override async Task<ProductInfoDto> Create(CreateProductInfoDto input)
        {
            var productInfo = ObjectMapper.Map<ProductInfo>(input);

            productInfo.Created = DateTime.Now;
            productInfo.IsEnabled = true;
            productInfo.UserId = (int)(base.AbpSession.UserId ?? 0);

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
            var all = from x in _productInfoRepository.GetAll()
                      select ObjectMapper.Map<ProductInfoDto>(x);

            var pagedResultDto = new PagedResultDto<ProductInfoDto>();
            pagedResultDto.Items = all.ToList().AsReadOnly();
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
            await File.WriteAllTextAsync(filePath, htmlContent);
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


    }
}