using System.Threading.Tasks;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Demo.Components.Toolbar.Search;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Demo.Components.Toolbar.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.Users;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Demo.Toolbars;

public class PureThemeDemoToolbarContributor : IToolbarContributor
{
    public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name != StandardToolbars.Main)
        {
            return Task.CompletedTask;
        }

        if (!(context.Theme is PureTheme))
        {
            return Task.CompletedTask;
        }


            context.Toolbar.Items.Insert(0,new ToolbarItem(typeof(SearchViewComponent)));
        context.Toolbar.Items.Insert(0, new ToolbarItem(typeof(NotificationsViewComponent)));

        return Task.CompletedTask;
    }
}