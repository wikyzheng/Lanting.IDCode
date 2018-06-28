using Abp.AutoMapper;
using Lanting.IDCode.Authentication.External;

namespace Lanting.IDCode.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
