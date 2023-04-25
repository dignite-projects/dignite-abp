using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Core;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.FontAwesome;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Bundling;

[DependsOn(
    typeof(CoreStyleContributor),
    typeof(BootstrapStyleContributor),
    typeof(FontAwesomeStyleContributor)
)]
public class PureThemePublicStyleContributor : BundleContributor
{    
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
    }
}
