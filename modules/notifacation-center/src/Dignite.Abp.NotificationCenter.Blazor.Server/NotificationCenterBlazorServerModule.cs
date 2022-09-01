using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Dignite.Abp.NotificationCenter.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(NotificationCenterBlazorModule)
        )]
    public class NotificationCenterBlazorServerModule : AbpModule
    {
        
    }
}