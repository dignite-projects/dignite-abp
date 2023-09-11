using System;
using Microsoft.Extensions.Localization;
using Volo.Abp.Localization;


namespace Dignite.Abp.Localization;

public class MultiTenancyStringLocalizerFactory : IStringLocalizerFactory
{
    private readonly MultiTenancyJsonStringLocalizerFactory _jsonStringLocalizerFactory;
    private readonly AbpStringLocalizerFactory _abpStringLocalizerFactory;

    public MultiTenancyStringLocalizerFactory(MultiTenancyJsonStringLocalizerFactory jsonStringLocalizerFactory, AbpStringLocalizerFactory abpStringLocalizerFactory)
    {
        _jsonStringLocalizerFactory = jsonStringLocalizerFactory;
        _abpStringLocalizerFactory = abpStringLocalizerFactory;
    }

    public IStringLocalizer Create(Type resourceSource)
    {
        if (MultiTenancyLocalizationResourceNameAttribute.GetOrNull(resourceSource) != null)
            return _jsonStringLocalizerFactory.Create(resourceSource);
        else
            return _abpStringLocalizerFactory.Create(resourceSource);
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        return _abpStringLocalizerFactory.Create(baseName, location);
    }
}
