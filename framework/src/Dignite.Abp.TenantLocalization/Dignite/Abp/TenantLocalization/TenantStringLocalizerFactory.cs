using System;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Volo.Abp.Localization;
using Volo.Abp.Localization.External;


namespace Dignite.Abp.TenantLocalization;

public class TenantStringLocalizerFactory : AbpStringLocalizerFactory
{
    private readonly TenantJsonStringLocalizerFactory _jsonStringLocalizerFactory;

    public TenantStringLocalizerFactory(
        TenantJsonStringLocalizerFactory jsonStringLocalizerFactory,
        ResourceManagerStringLocalizerFactory innerFactory,
        IOptions<AbpLocalizationOptions> abpLocalizationOptions,
        IServiceProvider serviceProvider,
        IExternalLocalizationStore externalLocalizationStore)
        :base(innerFactory,abpLocalizationOptions,serviceProvider,externalLocalizationStore)
    {
        _jsonStringLocalizerFactory = jsonStringLocalizerFactory;
    }

    public override IStringLocalizer Create(Type resourceSource)
    {
        if (TenantLocalizationResourceNameAttribute.GetOrNull(resourceSource) != null)
            return _jsonStringLocalizerFactory.CreateByMultiTenancy(resourceSource);
        else
            return base.Create(resourceSource);
    }
}
