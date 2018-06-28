using System.Threading.Tasks;
using Abp.Application.Services;
using Lanting.IDCode.Sessions.Dto;

namespace Lanting.IDCode.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
