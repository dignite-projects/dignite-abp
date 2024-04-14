using System.Threading.Tasks;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Components.LanguageSwitch;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Demo.Components.Toolbar.Notifications;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Demo.Components.Toolbar.Search;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Demo.Toolbars;

public class PureThemeDemoToolbarContributor : IToolbarContributor
{
    public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name == StandardToolbars.Main)
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(LanguageSwitchViewComponent)));
            context.Toolbar.Items.Add(new ToolbarItem(typeof(NotificationsViewComponent)));
            return Task.CompletedTask;
        }

        if (!(context.Theme is PureTheme))
        {
            return Task.CompletedTask;
        }

        if (context.Toolbar.Name == PureToolbars.BottomNavigationBar)
        {
            context.Toolbar.Items.Insert(0, new ToolbarItem(typeof(HomePageViewComponent)));
            context.Toolbar.Items.Add(new ToolbarItem(typeof(SearchViewComponent)));
            context.Toolbar.Items.Add(new ToolbarItem(typeof(NotificationsViewComponent)));
            return Task.CompletedTask;
        }

        if (context.Toolbar.Name == NavbarConsts.HomeTools)
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(SearchViewComponent)));
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}