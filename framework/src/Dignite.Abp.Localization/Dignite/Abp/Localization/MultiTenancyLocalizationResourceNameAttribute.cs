using System;
using System.Linq;

namespace Dignite.Abp.Localization;

public class MultiTenancyLocalizationResourceNameAttribute : Attribute
{

    public static MultiTenancyLocalizationResourceNameAttribute GetOrNull(Type resourceType)
    {
        return resourceType
            .GetCustomAttributes(true)
            .OfType<MultiTenancyLocalizationResourceNameAttribute>()
            .FirstOrDefault();
    }
}
