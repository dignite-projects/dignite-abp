using System.Threading.Tasks;
using Dignite.Abp.NotificationCenter.Blazor.Pages.NotificationCenter;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;

namespace Dignite.Abp.NotificationCenter.Blazor.Server;

public class NotificationCenterToolbarContributor : IToolbarContributor
{
    public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name == StandardToolbars.Main)
        {
            context.Toolbar.Items.Insert(0,new ToolbarItem(typeof(NotificationsTool)));
        }

        return Task.CompletedTask;
    }
}