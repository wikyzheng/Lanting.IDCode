using Abp.Application.Navigation;
using Abp.Localization;
using Lanting.IDCode.Authorization;

namespace Lanting.IDCode.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class IDCodeNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Tenants,
                        L("Tenants"),
                        url: "Tenants",
                        icon: "business",
                        requiredPermissionName: PermissionNames.Pages_Tenants
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "Users",
                        icon: "people",
                        requiredPermissionName: PermissionNames.Pages_Users
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Roles,
                        L("Roles"),
                        url: "Roles",
                        icon: "local_offer",
                        requiredPermissionName: PermissionNames.Pages_Roles
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Tasks,
                        L("Tasks"),
                        url: "Task",
                        icon: "event",
                        requiredPermissionName: PermissionNames.Pages_Codes
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Products,
                        L("Products"),
                        url: "Product",
                       icon: "settings",
                        requiredPermissionName: PermissionNames.Pages_Codes
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, IDCodeConsts.LocalizationSourceName);
        }
    }
}
