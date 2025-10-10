using System.IO;
using System.Linq;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Bundling;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Demo.Bundling;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Demo.Menus;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Demo.Toolbars;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.Toolbar.LoginLink;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Demo.Components.Toolbar.Notifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.DatatablesNet;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.DatatablesNetBs5;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Demo;

[DependsOn(
    typeof(AbpAspNetCoreMvcUiPureThemeModule),
    typeof(AbpAutofacModule)
    )]
public class AbpAspNetCoreMvcUiThemePureDemoModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var env = context.Services.GetHostingEnvironment();

        if (env.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<AbpAspNetCoreMvcUiPureThemeModule>(Path.Combine(env.ContentRootPath, string.Format("..{0}..{0}src{0}Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure", Path.DirectorySeparatorChar)));
            });
        }

        Configure<AbpBundlingOptions>(options =>
        {
            var standardStyleBundles = options.StyleBundles.Get(StandardBundles.Styles.Global);
            var standardScriptBundles = options.ScriptBundles.Get(StandardBundles.Styles.Global);

            /* remove datatable styles */
            standardStyleBundles.Contributors.Remove<DatatablesNetBs5StyleContributor>();
            standardStyleBundles.Contributors.Remove<SharedThemeGlobalStyleContributor>();

            /* remove datatable scripts */
            standardScriptBundles.Contributors.Remove<DatatablesNetScriptContributor>();
            standardScriptBundles.Contributors.Remove<DatatablesNetBs5ScriptContributor>();



            /*  */
            var globalStyleBundles = options.StyleBundles.Get(PureThemeBundles.Styles.Global);
            var globalScriptBundles = options.ScriptBundles.Get(PureThemeBundles.Scripts.Global);

            globalStyleBundles.AddContributors(typeof(PureThemeGlobalStyleDemoContributor));
            globalScriptBundles.AddContributors(typeof(PureThemeGlobalScriptDemoContributor));
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new PureThemeDemoMenuContributor());
        });

        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new PureThemeDemoToolbarContributor());
        });

        Configure<PureThemeMvcOptions>(options =>
        {
            options.MobileMenuSelector = items => items.Where(x => x.Name == PureThemeDemoMenus.Home || x.Name == PureThemeDemoMenus.Components.Root);
            options.MobileToolbarSelector = items => items.Where(x=> x.ComponentType == typeof(LoginLinkViewComponent) || x.ComponentType == typeof(NotificationsViewComponent));
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