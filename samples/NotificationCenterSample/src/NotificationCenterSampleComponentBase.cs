using NotificationCenterSample.Localization;
using Volo.Abp.AspNetCore.Components;

namespace NotificationCenterSample;

public abstract class NotificationCenterSampleComponentBase : AbpComponentBase
{
    protected NotificationCenterSampleComponentBase()
    {
        LocalizationResource = typeof(NotificationCenterSampleResource);
    }
}
