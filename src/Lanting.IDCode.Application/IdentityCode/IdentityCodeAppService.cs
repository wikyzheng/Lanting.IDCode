using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Authorization;
using Lanting.IDCode.Authorization;
using Lanting.IDCode.Entity;

namespace Lanting.IDCode.Application
{
    [AbpAuthorize(PermissionNames.Pages_Codes)]
    public partial class IdentityCodeAppService : AsyncCrudAppService<IdentityCode, IdentityCodeDto, long, PagedResultRequestDto, CreateIdentityCodeDto, IdentityCodeDto>, IIdentityCodeAppService
    {
        /// <summary>
        /// IdentityCodeRepository
        /// </summary>
        private readonly IRepository<IdentityCode, long> _identityCodeRepository;

        /// <summary>
        /// EventBus
        /// </summary>
        public IEventBus EventBus { get; set; }

        public IdentityCodeAppService(IRepository<IdentityCode, long> identityCodeRepository) : base(identityCodeRepository)
        {
            _identityCodeRepository = identityCodeRepository;

            EventBus = NullEventBus.Instance;
        }

        public override async Task<IdentityCodeDto> Create(CreateIdentityCodeDto input)
        {
            var identityCode = ObjectMapper.Map<IdentityCode>(input);

            var entity = _identityCodeRepository.Insert(identityCode);
            var dto = ObjectMapper.Map<IdentityCodeDto>(entity);

            return await Task.FromResult(dto);
        }

        public override Task<IdentityCodeDto> Get(EntityDto<long> input)
        {
            var entity = _identityCodeRepository.Get(input.Id);
            var dto = ObjectMapper.Map<IdentityCodeDto>(entity);
            return Task.FromResult<IdentityCodeDto>(dto);
        }

        public override async Task<IdentityCodeDto> Update(IdentityCodeDto input)
        {
            var identityCode = await _identityCodeRepository.SingleAsync(x => x.Id == input.Id);

            identityCode.Code = input.Code;
            identityCode.ProductId = input.ProductId;
            identityCode.AntiFakeCode = input.AntiFakeCode;
            identityCode.IsActived = input.IsActived;
            identityCode.Created = input.Created;
            identityCode.TaskId = input.TaskId;

            return ObjectMapper.Map<IdentityCodeDto>(identityCode);
        }

        public override async Task<PagedResultDto<IdentityCodeDto>> GetAll(PagedResultRequestDto input)
        {
            var all = from x in _identityCodeRepository.GetAll()
                      select ObjectMapper.Map<IdentityCodeDto>(x);

            var pagedResultDto = new PagedResultDto<IdentityCodeDto>();
            pagedResultDto.Items = all.ToList().AsReadOnly();
            pagedResultDto.TotalCount = all.Count();

            return await Task.FromResult(pagedResultDto);
        }
    }
}