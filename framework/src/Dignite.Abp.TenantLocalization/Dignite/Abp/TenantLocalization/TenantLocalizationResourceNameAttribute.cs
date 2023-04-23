using System;
using System.Linq;

namespace Dignite.Abp.TenantLocalization;

public class TenantLocalizationResourceNameAttribute : Attribute
{
    public string Name { get; }

    public TenantLocalizationResourceNameAttribute(string name)
    {
        Name = name;
    }

    public static TenantLocalizationResourceNameAttribute GetOrNull(Type resourceType)
    {
        return resourceType
            .GetCustomAttributes(true)
            .OfType<TenantLocalizationResourceNameAttribute>()
            .FirstOrDefault();
    }

    public static string GetName(Type resourceType)
    {
        return GetOrNull(resourceType)?.Name ?? resourceType.FullName;
    }
}
