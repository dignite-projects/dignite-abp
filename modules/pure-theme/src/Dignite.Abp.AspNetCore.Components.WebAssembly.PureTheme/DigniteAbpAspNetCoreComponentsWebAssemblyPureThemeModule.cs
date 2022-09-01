using Dignite.Abp.AspNetCore.Components.Web.PureTheme;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;
using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Http.Client.IdentityModel.WebAssembly;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AspNetCore.Components.WebAssembly.PureTheme
{
    [DependsOn(
        typeof(DigniteAbpAspNetCoreComponentsWebPureThemeModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule),
        typeof(AbpHttpClientIdentityModelWebAssemblyModule)
        )]
    public class DigniteAbpAspNetCoreComponentsWebAssemblyPureThemeModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(DigniteAbpAspNetCoreComponentsWebAssemblyPureThemeModule).Assembly);
            });

            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new PureThemeToolbarContributor());
            });
        }
    }
}
