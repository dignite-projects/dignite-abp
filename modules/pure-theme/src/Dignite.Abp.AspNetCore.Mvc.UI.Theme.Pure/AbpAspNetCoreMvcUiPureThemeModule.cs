using System.Text.Unicode;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Toolbars;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure;

[DependsOn(
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAspNetCoreMvcUiMultiTenancyModule)
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

        Configure<RazorViewEngineOptions>(options =>
        {
            var currentTenantLazy = context.Services.GetServiceLazy<ICurrentTenant>();
            options.ViewLocationExpanders.Add(new PureViewLocationExpander(currentTenantLazy));
        });

        Configure<WebEncoderOptions>(options =>
        {
            options.TextEncoderSettings = new System.Text.Encodings.Web.TextEncoderSettings(UnicodeRanges.All);
        });
    }
}