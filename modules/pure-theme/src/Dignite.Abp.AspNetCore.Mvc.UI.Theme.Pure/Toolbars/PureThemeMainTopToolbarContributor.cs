using System.Threading.Tasks;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.Toolbar.ColorModeSwitch;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.Toolbar.HomeLink;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.Toolbar.LanguageSwitch;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.Toolbar.LoginLink;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.Toolbar.More;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.Toolbar.UserMenu;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.Users;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Toolbars;
public class PureThemeMainTopToolbarContributor : IToolbarContributor
{
    public async Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name != StandardToolbars.Main)
        {
            return;
        }

        if (!(context.Theme is PureTheme))
        {
            return;
        }

        //Home link component
        context.Toolbar.Items.Add(new ToolbarItem(typeof(HomeLinkViewComponent)));

        //Current User component
        if (context.ServiceProvider.GetRequiredService<ICurrentUser>().IsAuthenticated)
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(UserMenuViewComponent)));
        }
        else
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(LoginLinkViewComponent)));
        }

        //Language Switch Component
        context.Toolbar.Items.Add(new ToolbarItem(typeof(LanguageSwitchViewComponent)));

        //Color Mode Switch Component
        context.Toolbar.Items.Add(new ToolbarItem(typeof(ColorModeSwitchViewComponent)));

        //Navbar Toggler Component
        context.Toolbar.Items.Add(new ToolbarItem(typeof(NavbarTogglerViewComponent)));
    }
}
