using Volo.Abp.Bundling;

namespace Dignite.Abp.AspNetCore.Components.WebAssembly.PureTheme
{
    public class PureThemeBundleContributor : IBundleContributor
    {
        public void AddScripts(BundleContext context)
        {
            context.Add("_content/Dignite.Abp.BlazoriseUI/libs/abp/js/blazorise.js");
            context.Add("_content/Dignite.Abp.AspNetCore.Components.Web.PureTheme/libs/abp/js/theme.js");
        }

        public void AddStyles(BundleContext context)
        {
            context.Add("_content/Dignite.Abp.BlazoriseUI/libs/abp/css/blazorise.css");
            context.Add("_content/Dignite.Abp.AspNetCore.Components.Web.PureTheme/libs/abp/css/theme.css");
        }
    }
}
