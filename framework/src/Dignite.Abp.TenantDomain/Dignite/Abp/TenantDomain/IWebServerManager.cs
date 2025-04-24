using System;
using System.Threading.Tasks;

namespace Dignite.Abp.TenantDomain;
public interface IWebServerManager
{
    Task AddOrUpdateDomainAsync(string domain, string upstreamAddress, Guid tenantId);
    Task<bool> CheckCertificateValidityAsync(string domain);
    Task RemoveDomainAsync(Guid tenantId);
}
