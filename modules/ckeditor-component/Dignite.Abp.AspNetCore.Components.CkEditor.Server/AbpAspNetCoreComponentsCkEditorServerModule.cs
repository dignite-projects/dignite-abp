using Dignite.Abp.AspNetCore.Components.CkEditor.Server.Bundling;
using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AspNetCore.Components.CkEditor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule)
    )]
public class AbpAspNetCoreComponentsCkEditorServerModule: AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options
                .ScriptBundles
                .Add(CkeditorBundles.Scripts.Ckeditor, bundle =>
                {
                    bundle
                        .AddContributors(typeof(CkeditorScriptContributor));
                });
        });
    }
}
