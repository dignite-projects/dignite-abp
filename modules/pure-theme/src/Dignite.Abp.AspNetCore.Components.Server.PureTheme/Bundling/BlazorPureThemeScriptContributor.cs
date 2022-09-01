using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Dignite.Abp.AspNetCore.Components.Server.PureTheme.Bundling
{
    public class BlazorPureThemeScriptContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.Add("/_content/Dignite.Abp.BlazoriseUI/libs/abp/js/blazorise.js");
            context.Files.Add("/_content/Dignite.Abp.AspNetCore.Components.Web.PureTheme/libs/abp/js/theme.js");
        }

    }
}