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

namespace Lanting.IDCode.Application
{
    [AbpAuthorize(PermissionNames.Pages_Codes)]
    public partial class ProductInfoAppService : AsyncCrudAppService<ProductInfo, ProductInfoDto, int, PagedResultRequestDto, CreateProductInfoDto, ProductInfoDto>, IProductInfoAppService
    {
        /// <summary>
        /// ProductInfoRepository
        /// </summary>
        private readonly IRepository<ProductInfo, int> _productInfoRepository;

        /// <summary>
        /// EventBus
        /// </summary>
        public IEventBus EventBus { get; set; }

        public ProductInfoAppService(IRepository<ProductInfo, int> productInfoRepository) : base(productInfoRepository)
        {
            _productInfoRepository = productInfoRepository;

            EventBus = NullEventBus.Instance;
        }

        public override async Task<ProductInfoDto> Create(CreateProductInfoDto input)
        {
            var productInfo = ObjectMapper.Map<ProductInfo>(input);

            var entity = _productInfoRepository.Insert(productInfo);
            var dto = ObjectMapper.Map<ProductInfoDto>(entity);

            return await Task.FromResult(dto);
        }

        public override Task<ProductInfoDto> Get(EntityDto<int> input)
        {
            var entity = _productInfoRepository.Get(input.Id);
            var dto = ObjectMapper.Map<ProductInfoDto>(entity);
            return Task.FromResult<ProductInfoDto>(dto);
        }

        public override async Task<ProductInfoDto> Update(ProductInfoDto input)
        {
            var productInfo = await _productInfoRepository.SingleAsync(x => x.Id == input.Id);

            productInfo.UserId = input.UserId;
            productInfo.Code = input.Code;
            productInfo.FullName = input.FullName;
            productInfo.IsEnabled = input.IsEnabled;
            productInfo.Description = input.Description;
            productInfo.Created = input.Created;
            productInfo.Modified = input.Modified;

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
    }
}