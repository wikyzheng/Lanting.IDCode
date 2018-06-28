using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Lanting.IDCode.MultiTenancy.Dto;

namespace Lanting.IDCode.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
