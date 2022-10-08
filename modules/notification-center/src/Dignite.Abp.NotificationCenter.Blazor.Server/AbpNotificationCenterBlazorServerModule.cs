using Dignite.Abp.NotificationCenter.Blazor.Server.Bundling;
using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.Modularity;

namespace Dignite.Abp.NotificationCenter.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(AbpNotificationCenterBlazorModule)
    )]
public class AbpNotificationCenterBlazorServerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new NotificationCenterToolbarContributor());
        });

        Configure<AbpBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Add(NotificationCenterBundles.Styles.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(NotificationCenterBundles.Styles.Global)
                        .AddContributors(typeof(NotificationCenterStyleContributor));
                });
        });
    }
}