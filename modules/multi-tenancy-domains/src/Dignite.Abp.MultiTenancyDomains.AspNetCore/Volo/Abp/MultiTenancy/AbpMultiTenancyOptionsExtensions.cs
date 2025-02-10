using Dignite.Abp.MultiTenancyDomains.AspNetCore;
using System.Collections.Generic;

namespace Volo.Abp.MultiTenancy;

public static class AbpMultiTenancyOptionsExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="ignoreDomains"></param>
    public static void AddMultiTenantDomainsTenantResolver(this AbpTenantResolveOptions options, string[]? ignoreDomains = null)
    {
        options.TenantResolvers.InsertAfter(
            r => r is CurrentUserTenantResolveContributor,
            new MultiTenancyDomainsTenantResolveContributor(ignoreDomains)
        );
    }
}
