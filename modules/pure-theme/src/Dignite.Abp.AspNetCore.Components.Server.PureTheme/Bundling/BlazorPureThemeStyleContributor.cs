using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Dignite.Abp.AspNetCore.Components.Server.PureTheme.Bundling
{
    public class BlazorPureThemeStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/_content/Dignite.Abp.BlazoriseUI/libs/abp/css/blazorise.css");
            context.Files.AddIfNotContains("/_content/Dignite.Abp.AspNetCore.Components.Web.PureTheme/libs/abp/css/theme.css");
        }
    }
}