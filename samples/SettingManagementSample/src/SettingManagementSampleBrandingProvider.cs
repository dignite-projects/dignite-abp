using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace SettingManagementSample;

[Dependency(ReplaceServices = true)]
public class SettingManagementSampleBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "SettingManagementSample";
}
