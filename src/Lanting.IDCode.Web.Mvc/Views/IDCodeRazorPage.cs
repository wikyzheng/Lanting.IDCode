using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;

namespace Lanting.IDCode.Web.Views
{
    public abstract class IDCodeRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected IDCodeRazorPage()
        {
            LocalizationSourceName = IDCodeConsts.LocalizationSourceName;
        }
    }
}
