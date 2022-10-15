using SettingManagementSample.Localization;
using Volo.Abp.AspNetCore.Components;

namespace SettingManagementSample;

public abstract class SettingManagementSampleComponentBase : AbpComponentBase
{
    protected SettingManagementSampleComponentBase()
    {
        LocalizationResource = typeof(SettingManagementSampleResource);
    }
}
