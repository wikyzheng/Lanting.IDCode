using System.Threading.Tasks;
using Abp.Application.Services;
using Lanting.IDCode.Authorization.Accounts.Dto;

namespace Lanting.IDCode.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
