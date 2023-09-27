using Dignite.Abp.Notifications.Components;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Dignite.Abp.NotificationCenter.Blazor;

[DependsOn(
    typeof(DigniteAbpNotificationCenterApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule),
    typeof(DigniteAbpNotificationsComponentsModule)
    )]
public class DigniteAbpNotificationCenterBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<DigniteAbpNotificationCenterBlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<NotificationCenterBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(DigniteAbpNotificationCenterBlazorModule).Assembly);
        });
    }
}