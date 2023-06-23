using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.JQuery;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Bundling;

[DependsOn(
    typeof(JQueryScriptContributor),
    typeof(BootstrapScriptContributor)
    )]
public class PureThemePublicScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Add("/themes/pure/public.js");
    }
}
