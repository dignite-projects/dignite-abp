using Microsoft.Extensions.DependencyInjection;
using Dignite.Abp.NotificationCenter.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.NotificationCenter.Blazor
{
    [DependsOn(
        typeof(NotificationCenterApplicationContractsModule),
        typeof(AbpAspNetCoreComponentsWebThemingModule),
        typeof(AbpAutoMapperModule)
        )]
    public class NotificationCenterBlazorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<NotificationCenterBlazorModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<NotificationCenterBlazorAutoMapperProfile>(validate: true);
            });

            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new NotificationCenterMenuContributor());
            });

            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(NotificationCenterBlazorModule).Assembly);
            });
        }
    }
}