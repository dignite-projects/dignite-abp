using Volo.Abp.Bundling;

namespace Dignite.Cms.Blazor.Host;

public class CmsBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
