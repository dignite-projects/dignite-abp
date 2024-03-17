using System;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Volo.Abp.Localization;
using Volo.Abp.Localization.External;


namespace Dignite.Abp.Localization;

public class MultiTenancyStringLocalizerFactory : AbpStringLocalizerFactory
{
    private readonly MultiTenancyJsonStringLocalizerFactory _jsonStringLocalizerFactory;

    public MultiTenancyStringLocalizerFactory(
        MultiTenancyJsonStringLocalizerFactory jsonStringLocalizerFactory,
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
        if (MultiTenancyLocalizationResourceNameAttribute.GetOrNull(resourceSource) != null)
            return _jsonStringLocalizerFactory.Create(resourceSource);
        else
            return base.Create(resourceSource);
    }
}
