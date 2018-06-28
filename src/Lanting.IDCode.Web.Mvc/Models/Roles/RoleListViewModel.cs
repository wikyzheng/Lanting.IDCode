using System.Collections.Generic;
using Lanting.IDCode.Roles.Dto;

namespace Lanting.IDCode.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }

        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
