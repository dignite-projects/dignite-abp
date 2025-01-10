using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Dignite.OfficialWebsite.Web.Public;

[Dependency(ReplaceServices = true)]
public class DigniteAbpAspNetCoreMvcUiThemePureDemoBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Dignite";
    public override string LogoUrl => "/images/logo.png";
    public override string LogoReverseUrl => "/images/logo-reverse.png";
}
