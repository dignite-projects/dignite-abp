using System;
using System.Threading.Tasks;

namespace Dignite.Abp.TenantDomain;
public interface IWebServerManager
{
    Task AddOrUpdateDomainAsync(string domain, string upstreamAddress, Guid tenantId, string site = null);
    Task<bool> CheckCertificateValidityAsync(string domain);
    Task RemoveDomainAsync(string domain, string site = null);
}
