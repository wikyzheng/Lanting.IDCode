using System.Threading.Tasks;
using Lanting.IDCode.Configuration.Dto;

namespace Lanting.IDCode.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
