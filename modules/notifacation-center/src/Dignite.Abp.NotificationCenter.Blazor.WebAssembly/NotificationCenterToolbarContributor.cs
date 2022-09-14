using System.Threading.Tasks;
using Dignite.Abp.NotificationCenter.Blazor.WebAssembly.Toolbar;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;

namespace Dignite.Abp.NotificationCenter.Blazor.WebAssembly;

public class NotificationCenterToolbarContributor : IToolbarContributor
{
    public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name == StandardToolbars.Main)
        {
            //TODO: Can we find a different way to understand if authentication was configured or not?
            var authenticationStateProvider = context.ServiceProvider
                .GetService<AuthenticationStateProvider>();

            if (authenticationStateProvider != null)
            {
                context.Toolbar.Items.Insert(0, new ToolbarItem(typeof(NotificationReminder)));
            }
        }

        return Task.CompletedTask;
    }
}