using Abp.AspNetCore.Mvc.ViewComponents;

namespace Lanting.IDCode.Web.Views
{
    public abstract class IDCodeViewComponent : AbpViewComponent
    {
        protected IDCodeViewComponent()
        {
            LocalizationSourceName = IDCodeConsts.LocalizationSourceName;
        }
    }
}
