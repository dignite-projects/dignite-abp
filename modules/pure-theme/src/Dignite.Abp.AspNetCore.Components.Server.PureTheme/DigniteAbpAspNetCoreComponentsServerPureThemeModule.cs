using Dignite.Abp.AspNetCore.Components.Server.PureTheme.Bundling;
using Dignite.Abp.AspNetCore.Components.Web.PureTheme;
using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.AspNetCore.Components.Server.Theming.Bundling;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AspNetCore.Components.Server.PureTheme
{
    [DependsOn(
        typeof(DigniteAbpAspNetCoreComponentsWebPureThemeModule),
        typeof(AbpAspNetCoreComponentsServerThemingModule)
        )]
    public class DigniteAbpAspNetCoreComponentsServerPureThemeModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new PureThemeToolbarContributor());
            });
            
            Configure<AbpBundlingOptions>(options =>
            {
                options
                    .StyleBundles
                    .Add(BlazorPureThemeBundles.Styles.Global, bundle =>
                    {
                        bundle
                            .AddBaseBundles(BlazorStandardBundles.Styles.Global)
                            .AddContributors(typeof(BlazorPureThemeStyleContributor));
                    });

                options
                    .ScriptBundles
                    .Add(BlazorPureThemeBundles.Scripts.Global, bundle =>
                    {
                        bundle
                            .AddBaseBundles(BlazorStandardBundles.Scripts.Global)
                            .AddContributors(typeof(BlazorPureThemeScriptContributor));
                    });
            });
        }
    }
}
