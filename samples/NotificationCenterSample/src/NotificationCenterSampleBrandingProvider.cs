using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace NotificationCenterSample;

[Dependency(ReplaceServices = true)]
public class NotificationCenterSampleBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "NotificationCenterSample";
}
