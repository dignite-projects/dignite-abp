using System;
using System.Linq;

namespace Dignite.Abp.Localization.MultiTenancy;

public class MultiTenancyLocalizationResourceNameAttribute : Attribute
{
    public string Name { get; }

    public MultiTenancyLocalizationResourceNameAttribute(string name)
    {
        Name = name;
    }

    public static MultiTenancyLocalizationResourceNameAttribute GetOrNull(Type resourceType)
    {
        return resourceType
            .GetCustomAttributes(true)
            .OfType<MultiTenancyLocalizationResourceNameAttribute>()
            .FirstOrDefault();
    }

    public static string GetName(Type resourceType)
    {
        return GetOrNull(resourceType)?.Name ?? resourceType.FullName;
    }
}
