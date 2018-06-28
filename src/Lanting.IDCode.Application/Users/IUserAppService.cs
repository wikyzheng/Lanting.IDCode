using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Lanting.IDCode.Roles.Dto;
using Lanting.IDCode.Users.Dto;

namespace Lanting.IDCode.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
