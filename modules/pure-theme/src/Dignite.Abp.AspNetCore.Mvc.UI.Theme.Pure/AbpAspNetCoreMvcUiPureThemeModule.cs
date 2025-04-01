using System.Text.Unicode;
using Dignite.Abp.AspNetCore.Mvc.UI.MultiTenancyTheme;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Bundling;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Toolbars;
using Dignite.Abp.Seo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure;
[DependsOn(
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
    typeof(AbpAspNetCoreMvcUiMultiTenancyThemeModule),
    typeof(AbpSeoModule)
    )]
public class AbpAspNetCoreMvcUiPureThemeModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpAspNetCoreMvcUiPureThemeModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpThemingOptions>(options =>
        {
            options.Themes.Add<PureTheme>();

            if (options.DefaultThemeName == null)
            {
                options.DefaultThemeName = PureTheme.Name;
            }
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpAspNetCoreMvcUiPureThemeModule>("Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure");
        });

        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new PureThemeMainTopToolbarContributor());
        });

        Configure<AbpBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Add(PureThemeBundles.Styles.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(StandardBundles.Styles.Global)
                        .AddContributors(typeof(PureThemeGlobalStyleContributor));
                });

            options
                .ScriptBundles
                .Add(PureThemeBundles.Scripts.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(StandardBundles.Scripts.Global)
                        .AddContributors(typeof(PureThemeGlobalScriptContributor));
                });
        });


        Configure<WebEncoderOptions>(options =>
        {
            options.TextEncoderSettings = new System.Text.Encodings.Web.TextEncoderSettings(UnicodeRanges.All);
        });
    }
}