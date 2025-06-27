using System;
using System.Linq;

namespace Dignite.Abp.TenantLocalization;

public class TenantLocalizationResourceNameAttribute : Attribute
{

    public static TenantLocalizationResourceNameAttribute GetOrNull(Type resourceType)
    {
        return resourceType
            .GetCustomAttributes(true)
            .OfType<TenantLocalizationResourceNameAttribute>()
            .FirstOrDefault();
    }
}
