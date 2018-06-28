using Abp.AutoMapper;
using Lanting.IDCode.Sessions.Dto;

namespace Lanting.IDCode.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}
