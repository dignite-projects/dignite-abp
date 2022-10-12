using PureTheme.BlazorServerSample.Localization;
using Volo.Abp.AspNetCore.Components;

namespace PureTheme.BlazorServerSample;

public abstract class BlazorServerSampleComponentBase : AbpComponentBase
{
    protected BlazorServerSampleComponentBase()
    {
        LocalizationResource = typeof(BlazorServerSampleResource);
    }
}
