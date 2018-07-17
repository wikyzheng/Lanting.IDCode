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
using Abp.UI;

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

            var isExist = _generateTaskRepository.GetAll().Any(x => x.Remark.Equals(input.Remark, StringComparison.OrdinalIgnoreCase));
            if (isExist)
                throw new UserFriendlyException("任务名重复！");

            //generate the task
            input.Created = DateTime.Now;
            input.TaskStatu = (int)TaskStatu.Init;
            input.UserId = (int)(base.AbpSession.UserId ?? 0);
            input.IsAntiFake = input.AFCodeLength.HasValue && input.AFCodeLength.Value > 0;
            input.IsSuccess = false;
            var generateTask = ObjectMapper.Map<GenerateTask>(input);
            var entity = await _generateTaskRepository.InsertAndGetIdAsync(generateTask);

            //run the task


            return new GenerateTaskDto();
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

            generateTask.ProductId = input.ProductId;
            generateTask.GenerateCount = input.GenerateCount;
            generateTask.TaskStatu = (TaskStatu)input.TaskStatu;
            generateTask.DataFilePath = input.DataFilePath;
            generateTask.IsAntiFake = input.IsAntiFake;
            generateTask.AFCodeLength = input.AFCodeLength;
            generateTask.AntiFackCodeType = (AntiFackCodeType)input.AntiFackCodeType;

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