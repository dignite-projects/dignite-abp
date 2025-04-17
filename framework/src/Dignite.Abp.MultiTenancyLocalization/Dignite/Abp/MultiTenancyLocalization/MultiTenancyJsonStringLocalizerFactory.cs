using System;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using My.Extensions.Localization.Json;
using My.Extensions.Localization.Json.Internal;
using Volo.Abp.MultiTenancy;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Dignite.Abp.MultiTenancyLocalization;

public class MultiTenancyJsonStringLocalizerFactory : JsonStringLocalizerFactory
{
    private readonly ConcurrentDictionary<string, JsonStringLocalizer> _localizerCache = new ConcurrentDictionary<string, JsonStringLocalizer>();
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

    /// <summary>
    /// This method replaces the <see cref="JsonStringLocalizerFactory.Create(Type)"></see> method, which caches data by tenant.
    /// </summary>
    /// <param name="resourceSource"></param>
    /// <returns></returns>
    public IStringLocalizer CreateByMultiTenancy(Type resourceSource)
    {
        var typeInfo = resourceSource.GetTypeInfo();
        var assembly = typeInfo.Assembly;
        var assemblyName = resourceSource.Assembly.GetName().Name;
        var typeName = $"{assemblyName}.{typeInfo.Name}" == typeInfo.FullName
            ? typeInfo.Name
            : TrimPrefix(typeInfo.FullName, assemblyName + ".");

        var resourcesPath = Path.Combine(PathHelpers.GetApplicationRoot(), GetResourcePath(assembly));
        typeName = TryFixInnerClassPath(typeName);

        return _localizerCache.GetOrAdd(GetLocalizerCacheKey(typeName), _ => CreateJsonStringLocalizer(resourcesPath, typeName));
    }

    protected override JsonStringLocalizer CreateJsonStringLocalizer(string resourcesPath, string resourceName)
    {
        if (_currentTenant.IsAvailable)
        {
            resourceName = _currentTenant.Id + "." + resourceName;
            return base.CreateJsonStringLocalizer(resourcesPath, resourceName);
        }
        else
        {
            resourcesPath = PathHelpers.GetApplicationRoot();
            return base.CreateJsonStringLocalizer(resourcesPath, resourceName);
        }
    }

    private string GetResourcePath(Assembly assembly)
    {
        var resourceLocationAttribute = assembly.GetCustomAttribute<ResourceLocationAttribute>();

        return resourceLocationAttribute == null
            ? _localizationOptions.ResourcesPath
            : resourceLocationAttribute.ResourceLocation;
    }

    private static string TrimPrefix(string name, string prefix)
    {
        if (name.StartsWith(prefix, StringComparison.Ordinal))
        {
            return name.Substring(prefix.Length);
        }

        return name;
    }

    private string TryFixInnerClassPath(string path)
    {
        const char innerClassSeparator = '+';
        var fixedPath = path;

        if (path.Contains(innerClassSeparator.ToString()))
        {
            fixedPath = path.Replace(innerClassSeparator, '.');
        }

        return fixedPath;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="typeName"></param>
    /// <returns></returns>
    private string GetLocalizerCacheKey(string typeName)
    {
        string key = $"culture={CultureInfo.CurrentUICulture.Name}, typeName={typeName}";
        if (_currentTenant.Id.HasValue)
        {
            key += $", tenant={_currentTenant.Id.Value}";
        }
        return key;
    }
}

