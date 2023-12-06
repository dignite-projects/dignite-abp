using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.JQuery;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.JQueryForm;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.JQueryValidationUnobtrusive;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Lodash;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.SweetAlert2;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Toastr;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Bundling;

[DependsOn(
    typeof(JQueryScriptContributor),
    typeof(BootstrapScriptContributor),
    typeof(LodashScriptContributor),
    typeof(JQueryValidationUnobtrusiveScriptContributor),
    typeof(JQueryFormScriptContributor),
    typeof(Sweetalert2ScriptContributor),
    typeof(ToastrScriptBundleContributor)
    )]
public class PureThemePublicScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Add("/themes/pure/public.js");
    }
}
