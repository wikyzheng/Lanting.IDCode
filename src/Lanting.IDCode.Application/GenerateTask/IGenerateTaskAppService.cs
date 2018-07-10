using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lanting.IDCode.Application
{
    public interface IGenerateTaskAppService : IAsyncCrudAppService<GenerateTaskDto, int, PagedResultRequestDto, CreateGenerateTaskDto, GenerateTaskDto>
    {
     
    }
}