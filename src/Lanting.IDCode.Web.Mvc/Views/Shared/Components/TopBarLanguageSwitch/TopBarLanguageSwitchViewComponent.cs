using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Abp.Localization;

namespace Lanting.IDCode.Web.Views.Shared.Components.TopBarLanguageSwitch
{
    public class TopBarLanguageSwitchViewComponent : IDCodeViewComponent
    {
        private readonly ILanguageManager _languageManager;

        public TopBarLanguageSwitchViewComponent(ILanguageManager languageManager)
        {
            _languageManager = languageManager;
        }

        public IViewComponentResult Invoke()
        {
            var model = new TopBarLanguageSwitchViewModel
            {
                CurrentLanguage = _languageManager.CurrentLanguage,
                Languages = _languageManager.GetLanguages().Where(l => !l.IsDisabled).ToList()
            };

            return View(model);
        }
    }
}
