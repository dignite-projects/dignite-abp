using Microsoft.Extensions.DependencyInjection;
using Dignite.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Dignite.Abp.FieldCustomizing.Blazor;

namespace Dignite.Abp.SettingManagement.Blazor
{
    [DependsOn(
        typeof(DigniteAbpSettingManagementApplicationContractsModule),
        typeof(AbpAspNetCoreComponentsWebThemingModule),
        typeof(AbpAutoMapperModule),
        typeof(DigniteAbpFieldCustomizingBlazorComponentsModule)
        )]
    public class DigniteAbpSettingManagementBlazorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<DigniteAbpSettingManagementBlazorModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<SettingManagementBlazorAutoMapperProfile>(validate: true);
            });

            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new SettingManagementMenuContributor());
            });

            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(DigniteAbpSettingManagementBlazorModule).Assembly);
            });
        }
    }
}