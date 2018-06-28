using System.Collections.Generic;
using Lanting.IDCode.Roles.Dto;
using Lanting.IDCode.Users.Dto;

namespace Lanting.IDCode.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
