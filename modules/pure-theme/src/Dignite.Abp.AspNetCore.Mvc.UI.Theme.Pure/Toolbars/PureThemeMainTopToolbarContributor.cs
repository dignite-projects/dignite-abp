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

        //首页
        context.Toolbar.Items.Add(new ToolbarItem(typeof(HomeLinkViewComponent)));

        //当前用户
        if (context.ServiceProvider.GetRequiredService<ICurrentUser>().IsAuthenticated)
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(UserMenuViewComponent)));
        }
        else
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(LoginLinkViewComponent)));
        }

        //语言切换
        context.Toolbar.Items.Add(new ToolbarItem(typeof(LanguageSwitchViewComponent)));

        //主题色
        context.Toolbar.Items.Add(new ToolbarItem(typeof(ColorModeSwitchViewComponent)));

        //移动端模式下的更多菜单
        context.Toolbar.Items.Add(new ToolbarItem(typeof(NavbarTogglerViewComponent)));
    }
}
