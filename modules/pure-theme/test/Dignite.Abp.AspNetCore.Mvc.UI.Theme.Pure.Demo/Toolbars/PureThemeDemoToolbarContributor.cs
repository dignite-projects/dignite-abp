using System.Threading.Tasks;
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
            context.Toolbar.Items.Add(new ToolbarItem(typeof(NotificationsViewComponent)));
            context.Toolbar.Items.Add(new ToolbarItem(typeof(SearchViewComponent)));
            return Task.CompletedTask;
        }

        if (!(context.Theme is PureTheme))
        {
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}