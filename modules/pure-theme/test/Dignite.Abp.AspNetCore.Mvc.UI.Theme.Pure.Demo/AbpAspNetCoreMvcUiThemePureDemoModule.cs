using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Demo.Menus;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Demo;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Demo;

[DependsOn(
    typeof(AbpAspNetCoreMvcUIPureThemeModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedDemoModule),
    typeof(AbpAutofacModule)
    )]
public class AbpAspNetCoreMvcUiThemePureDemoModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var env = context.Services.GetHostingEnvironment();

        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles
                .Get(StandardBundles.Styles.Global)
                .AddFiles("/demo/styles/main.css");
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new PureThemeDemoMenuContributor());
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseConfiguredEndpoints();
    }
}