using Abp.Authorization;
using Lanting.IDCode.Authorization.Roles;
using Lanting.IDCode.Authorization.Users;

namespace Lanting.IDCode.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
