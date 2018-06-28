using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Lanting.IDCode.Configuration.Dto;

namespace Lanting.IDCode.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : IDCodeAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
