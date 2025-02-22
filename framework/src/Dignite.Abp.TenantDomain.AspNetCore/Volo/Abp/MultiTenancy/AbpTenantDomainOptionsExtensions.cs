using Dignite.Abp.TenantDomainManagement.AspNetCore;
using System.Collections.Generic;

namespace Volo.Abp.MultiTenancy;

public static class AbpTenantDomainOptionsExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public static void AddProxyHeaderTenantResolver(this AbpTenantResolveOptions options)
    {
        options.TenantResolvers.InsertAfter(
            r => r is CurrentUserTenantResolveContributor,
            new ProxyHeaderTenantResolveContributor()
        );
    }
}
