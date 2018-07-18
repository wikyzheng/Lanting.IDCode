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
using Lanting.IDCode.Core.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Lanting.IDCode.Sessions;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Lanting.IDCode.Application
{
    [AbpAuthorize(PermissionNames.Pages_Codes)]
    public partial class GenerateTaskAppService : AsyncCrudAppService<GenerateTask, GenerateTaskDto, int, PagedResultRequestDto, CreateGenerateTaskDto, GenerateTaskDto>, IGenerateTaskAppService
    {
        /// <summary>
        /// GenerateTaskRepository
        /// </summary>
        private readonly IRepository<GenerateTask, int> _generateTaskRepository;
        private readonly IIDentityCodeRepository _identityCodeRepository;
        private readonly IRepository<ProductInfo> _productRepository;
        private readonly ISessionAppService _sessionAppService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _fileDiretory = "codefile";
        private string _defaultUrl { get; set; }
        private readonly IConfiguration _configuration;
        /// <summary>
        /// EventBus
        /// </summary>
        public IEventBus EventBus { get; set; }

        public GenerateTaskAppService(IRepository<GenerateTask, int> generateTaskRepository, IIDentityCodeRepository identityCodeRepository, IRepository<ProductInfo> productRepository, ISessionAppService sessionAppService, IHostingEnvironment hostingEnvironment, IConfiguration configuration) : base(generateTaskRepository)
        {
            _generateTaskRepository = generateTaskRepository;
            _identityCodeRepository = identityCodeRepository;
            _productRepository = productRepository;
            EventBus = NullEventBus.Instance;
            _sessionAppService = sessionAppService;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _defaultUrl = _configuration.GetSection("DefaultUrl").Value;
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
            input.AntiFackCodeType = (int)AntiFackCodeType.Letter;
            var generateTask = ObjectMapper.Map<GenerateTask>(input);
            var taskId = await _generateTaskRepository.InsertAndGetIdAsync(generateTask);
            var currentTask = await _generateTaskRepository.GetAsync(taskId);

            try
            {
                var maxOne = await _identityCodeRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefaultAsync();

                long maxIndex = maxOne == null ? 0 : maxOne.Id;

                long startIndex = maxIndex;

                List<IdentityCode> codes = new List<IdentityCode>();

                //run the task
                for (int i = 0; i < input.GenerateCount; i++)
                {
                    var random = this.GenerateRandomCode(12);
                    maxIndex++;
                    IdentityCode code = new IdentityCode()
                    {
                        Id = maxIndex,
                        ComfuseCode = random,
                        AntiFakeCode = input.IsAntiFake ? GenerateRandomCode(input.AFCodeLength.Value) : string.Empty,
                        Created = input.Created,
                        IsActived = true,
                        ProductId = input.ProductId,
                        ScanTimes = 0,
                        TaskId = taskId,
                        Code = $"{random.Substring(0, 6)}{maxIndex.ToString().PadLeft(8, '0')}{random.Substring(6)}",
                    };
                    codes.Add(code);
                }

                var codeNums = codes.Select(x => $"{_defaultUrl}{x.Code}");

                var fileUrl = await this.GenerateFile(input.Remark, codeNums);

                await _identityCodeRepository.BatchInsert(codes);

                currentTask.StartOne = startIndex;
                currentTask.EndOne = maxIndex;
                currentTask.TaskStatu = TaskStatu.Completed;
                currentTask.IsSuccess = true;
                currentTask.DataFilePath = fileUrl;


            }
            catch (Exception ex)
            {
                currentTask.IsSuccess = false;
                currentTask.TaskStatu = TaskStatu.Completed;
                currentTask.FailReason = ex.Message;
            }

            var result = await _generateTaskRepository.UpdateAsync(currentTask);

            return ObjectMapper.Map<GenerateTaskDto>(result);
        }


        public string GenerateRandomCode(int length)
        {
            StringBuilder sb = new StringBuilder();
            Random r = new Random(System.Environment.TickCount);
            for (int i = 0; i < length; i++)
            {
                sb.Append(r.Next(9).ToString());
            }
            return sb.ToString();

        }

        public override Task<GenerateTaskDto> Get(EntityDto<int> input)
        {
            var entity = _generateTaskRepository.Get(input.Id);
            entity.Product = _productRepository.Get(entity.ProductId);
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
            var all = from x in _generateTaskRepository.GetAll().Include(x => x.Product)
                      select ObjectMapper.Map<GenerateTaskDto>(x);

            var pagedResultDto = new PagedResultDto<GenerateTaskDto>();
            pagedResultDto.Items = all.ToList().AsReadOnly();
            pagedResultDto.TotalCount = all.Count();

            return await Task.FromResult(pagedResultDto);
        }

        public async Task<string> GenerateFile(string remark, IEnumerable<string> fileContents)
        {
            var user = await _sessionAppService.GetCurrentLoginInformations();
            var fileName = $"{remark}.txt";

            string dir = Path.Combine(_hostingEnvironment.WebRootPath, _fileDiretory, user.User.UserName);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            string filePath = Path.Combine(dir, fileName);
            await File.AppendAllLinesAsync(filePath, fileContents);
            //http://xxx/codefile/baolong/remark.txt
            return $"{_defaultUrl}{_fileDiretory}/{user.User.UserName}/{fileName}";
        }
    }
}