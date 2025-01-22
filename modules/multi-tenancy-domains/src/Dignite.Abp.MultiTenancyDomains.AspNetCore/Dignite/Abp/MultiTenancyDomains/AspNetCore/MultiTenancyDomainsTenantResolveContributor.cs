using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.MultiTenancyDomains.AspNetCore;

public class MultiTenancyDomainsTenantResolveContributor : HttpTenantResolveContributorBase
{
    public const string ContributorName = "TenantDomain";

    public override string Name => ContributorName;

    public string[]? IgnoreDomains;

    public MultiTenancyDomainsTenantResolveContributor(string[]? ignoreDomains =null)
    {
        IgnoreDomains = ignoreDomains;
    }

    protected override async Task<string?> GetTenantIdOrNameFromHttpContextOrNullAsync(ITenantResolveContext context, HttpContext httpContext)
    {
        if (!httpContext.Request.Host.HasValue)
        {
            return null;
        }

        var hostName = httpContext.Request.Host.Value;

        if (IgnoreDomains != null && IgnoreDomains.Any(x=>x.Equals(hostName,StringComparison.OrdinalIgnoreCase)))
        { 
            return null;
        }

        using (var scope = context.ServiceProvider.CreateScope())
        {
            var tenantDomainAppService = scope.ServiceProvider.GetRequiredService<ITenantDomainAppService>();
            var tenantDomain = await tenantDomainAppService.FindByDomainNameAsync(hostName);
            if (tenantDomain == null)
                return null;
            else
            {
                context.Handled = true;
                return tenantDomain.TenantId?.ToString();
            }
        }
    }
}