using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Demo.Bundling;

public class PureThemeGlobalStyleDemoContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Add("/demo/styles/main.css");
    }
}