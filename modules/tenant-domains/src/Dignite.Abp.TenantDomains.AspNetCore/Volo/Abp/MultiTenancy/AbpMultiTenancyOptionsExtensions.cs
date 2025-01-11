﻿using Dignite.Abp.TenantDomains.AspNetCore;
using System.Collections.Generic;

namespace Volo.Abp.MultiTenancy;

public static class AbpMultiTenancyOptionsExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public static void AddTenantDomainTenantResolver(this AbpTenantResolveOptions options)
    {
        options.TenantResolvers.InsertAfter(
            r => r is CurrentUserTenantResolveContributor,
            new TenantDomainTenantResolveContributor()
        );
    }
}
