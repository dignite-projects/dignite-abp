using PureTheme.BlazorServerSample.Localization;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;

namespace PureTheme.BlazorServerSample.Menus;

public class BlazorServerSampleMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        if (context.Menu.Name == StandardMenus.Shortcut)
        {
            await ConfigureShortcutMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<BlazorServerSampleResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                BlazorServerSampleMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home",
                order: 0
            )
        );

        if (BlazorServerSampleModule.IsMultiTenant)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        return Task.CompletedTask;
    }
    private Task ConfigureShortcutMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<BlazorServerSampleResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                BlazorServerSampleMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home",
                order: 0
            )
        );
        context.Menu.Items.Insert(
            1,
            new ApplicationMenuItem(
                BlazorServerSampleMenus.Shortcut,
                l["Menu:Shortcut"],
                "/",
                icon: "fas fa-car",
                order: 1
            )
        );

        return Task.CompletedTask;
    }
}
