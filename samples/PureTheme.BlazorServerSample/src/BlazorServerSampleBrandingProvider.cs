using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace PureTheme.BlazorServerSample;

[Dependency(ReplaceServices = true)]
public class BlazorServerSampleBrandingProvider : DefaultBrandingProvider
{
    public override string LogoUrl => "/logos/dignite-small.png";
    public override string LogoReverseUrl => "/logos/dignite-small-reverse.png";
    public override string AppName => "BlazorServerSample";
}
