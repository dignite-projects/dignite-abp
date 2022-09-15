using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Demo.Bundling;

public class PureThemeGlobalScriptDemoContributor : BundleContributor
{

    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.RemoveAll(f => f == "/libs/abp/aspnetcore-mvc-ui-theme-shared/datatables/datatables-extensions.js");
    }
}