﻿using System.Text.Unicode;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.MultiTenancy;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Bundling;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.DatatablesNet;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.DatatablesNetBs5;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure;
[DependsOn(
    typeof(AbpAspNetCoreMvcUiBasicThemeModule),
    typeof(AbpAspNetCoreMvcUiThemeMultiTenancyModule)
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

        Configure<AbpBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Add(PureThemeBundles.Styles.Public, bundle =>
                {
                    bundle
                        .AddContributors(typeof(PureThemePublicStyleContributor));
                });

            options
                .ScriptBundles
                .Add(PureThemeBundles.Scripts.Pubilc, bundle =>
                {
                    bundle
                        .AddContributors(typeof(PureThemePublicScriptContributor));
                });
        });


        Configure<WebEncoderOptions>(options =>
        {
            options.TextEncoderSettings = new System.Text.Encodings.Web.TextEncoderSettings(UnicodeRanges.All);
        });
    }
}