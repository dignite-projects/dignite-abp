using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using My.Extensions.Localization.Json;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.Localization;

public class MultiTenancyJsonStringLocalizerFactory : JsonStringLocalizerFactory
{
    private readonly ICurrentTenant _currentTenant;
    private readonly JsonLocalizationOptions _localizationOptions;
    public MultiTenancyJsonStringLocalizerFactory(
        ICurrentTenant currentTenant,
        IOptions<JsonLocalizationOptions> localizationOptions,
        ILoggerFactory loggerFactory) : base(localizationOptions, loggerFactory)
    {
        _currentTenant = currentTenant;
        _localizationOptions = localizationOptions.Value;
    }

    protected override JsonStringLocalizer CreateJsonStringLocalizer(string resourcesPath, string resourceName)
    {
        if (_currentTenant.Id.HasValue)
        {
            resourceName = _currentTenant.Name + "." + resourceName;
            return base.CreateJsonStringLocalizer(resourcesPath, resourceName);
        }
        else
        {
            resourcesPath = resourcesPath.RemovePostFix($"\\{_localizationOptions.ResourcesPath}"); //Host language packs are placed in the root directory
            return base.CreateJsonStringLocalizer(resourcesPath, resourceName);
        }
    }
}

