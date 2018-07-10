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
    public partial class GenerateTaskAppService : AsyncCrudAppService<GenerateTask, GenerateTaskDto, int, PagedResultRequestDto, CreateGenerateTaskDto, GenerateTaskDto>, IGenerateTaskAppService
    {
        /// <summary>
        /// GenerateTaskRepository
        /// </summary>
        private readonly IRepository<GenerateTask, int> _generateTaskRepository;

        /// <summary>
        /// EventBus
        /// </summary>
        public IEventBus EventBus { get; set; }

        public GenerateTaskAppService(IRepository<GenerateTask, int> generateTaskRepository) : base(generateTaskRepository)
        {
            _generateTaskRepository = generateTaskRepository;
            EventBus = NullEventBus.Instance;
        }

        public override async Task<GenerateTaskDto> Create(CreateGenerateTaskDto input)
        {
            var generateTask = ObjectMapper.Map<GenerateTask>(input);

            var entity = _generateTaskRepository.Insert(generateTask);
            var dto = ObjectMapper.Map<GenerateTaskDto>(entity);

            return await Task.FromResult(dto);
        }

        public override Task<GenerateTaskDto> Get(EntityDto<int> input)
        {
            var entity = _generateTaskRepository.Get(input.Id);
            var dto = ObjectMapper.Map<GenerateTaskDto>(entity);
            return Task.FromResult<GenerateTaskDto>(dto);
        }

        public override async Task<GenerateTaskDto> Update(GenerateTaskDto input)
        {
            var generateTask = await _generateTaskRepository.SingleAsync(x => x.Id == input.Id);

            generateTask.UserId = input.UserId;
            generateTask.Remark = input.Remark;
            generateTask.Created = input.Created;
            generateTask.IsSuccess = input.IsSuccess;
            generateTask.FailReason = input.FailReason;
            generateTask.Completed = input.Completed;
            generateTask.ProductId = input.ProductId;
            generateTask.GenerateCount = input.GenerateCount;
            generateTask.TaskStatu = (TaskStatu)input.TaskStatu;
            generateTask.DataFilePath = input.DataFilePath;
            generateTask.IsAntiFake = input.IsAntiFake;
            generateTask.AFCodeLength = input.AFCodeLength;
            generateTask.AntiFackCodeType = (AntiFackCodeType)input.AntiFackCodeType;
            generateTask.AntiFackCode = input.AntiFackCode;
            generateTask.StartOne = input.StartOne;
            generateTask.EndOne = input.EndOne;

            return ObjectMapper.Map<GenerateTaskDto>(generateTask);
        }

        public override async Task<PagedResultDto<GenerateTaskDto>> GetAll(PagedResultRequestDto input)
        {
            var all = from x in _generateTaskRepository.GetAll()
                      select ObjectMapper.Map<GenerateTaskDto>(x);

            var pagedResultDto = new PagedResultDto<GenerateTaskDto>();
            pagedResultDto.Items = all.ToList().AsReadOnly();
            pagedResultDto.TotalCount = all.Count();

            return await Task.FromResult(pagedResultDto);
        }
    }
}