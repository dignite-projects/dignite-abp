using Dignite.Abp.SettingManagement.Localization;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.SettingManagement.Blazor.Menus
{
    public class SettingManagementMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var administrationMenu = context.Menu.GetAdministration();
            var l = context.GetLocalizer<DigniteAbpSettingManagementResource>();

            administrationMenu.AddItem(new ApplicationMenuItem(
                    SettingManagementMenus.Prefix,
                    l["SettingManagement"],
                    url: "~/setting-management/global-settings",
                    icon: "fa fa-cog")
                .RequirePermissions(SettingManagementPermissions.Global));

            administrationMenu.AddItem(new ApplicationMenuItem(
                    SettingManagementMenus.Prefix,
                    l["SettingManagement"],
                    url: "~/setting-management/tenant-settings",
                    icon: "fa fa-cog")
                .RequirePermissions(SettingManagementPermissions.Tenant));

            return Task.CompletedTask;
        }
    }
}