using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using My.Extensions.Localization.Json;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.Localization;

public class MultiTenancyJsonStringLocalizerFactory : JsonStringLocalizerFactory
{
    private readonly ICurrentTenant _currentTenant;
    public MultiTenancyJsonStringLocalizerFactory(
        ICurrentTenant currentTenant,
        IOptions<JsonLocalizationOptions> localizationOptions,
        ILoggerFactory loggerFactory) : base(localizationOptions, loggerFactory)
    {
        _currentTenant = currentTenant;
    }

    protected override JsonStringLocalizer CreateJsonStringLocalizer(string resourcesPath, string resourceName)
    {
        if (!_currentTenant.Name.IsNullOrEmpty())
        {
            resourcesPath = Path.Combine(resourcesPath, _currentTenant.Name);
        }
        return base.CreateJsonStringLocalizer(resourcesPath, resourceName);
    }
}

