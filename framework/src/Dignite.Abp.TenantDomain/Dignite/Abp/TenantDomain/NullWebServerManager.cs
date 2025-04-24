using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Dignite.Abp.TenantDomain;
public class NullWebServerManager(ILogger<WebServerManagerBase> logger) : WebServerManagerBase(logger)
{
    public override Task AddOrUpdateDomainAsync(string domain, string upstreamAddress, Guid tenantId)
    {
        Logger.LogWarning("USING NullWebServerManager!");
        Logger.LogDebug("AddOrUpdateDomainAsync:");
        Logger.LogDebug(domain);
        Logger.LogDebug(upstreamAddress);
        return Task.FromResult(0);
    }

    public override Task<bool> CheckCertificateValidityAsync(string domain)
    {
        Logger.LogWarning("USING NullWebServerManager!");
        Logger.LogDebug("CheckCertificateValidityAsync:");
        Logger.LogDebug(domain);
        return Task.FromResult(false);
    }

    public override Task RemoveDomainAsync(Guid tenantId)
    {
        Logger.LogWarning("USING NullWebServerManager!");
        Logger.LogDebug("RemoveDomainAsync");
        Logger.LogDebug(tenantId.ToString());
        return Task.FromResult(0);
    }
}
