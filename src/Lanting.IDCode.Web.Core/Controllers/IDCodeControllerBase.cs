using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Lanting.IDCode.Sessions;
using Microsoft.AspNetCore.Identity;

namespace Lanting.IDCode.Controllers
{
    public abstract class IDCodeControllerBase: AbpController
    {
       
        protected IDCodeControllerBase()
        {
            LocalizationSourceName = IDCodeConsts.LocalizationSourceName;
           
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }


    }
}
