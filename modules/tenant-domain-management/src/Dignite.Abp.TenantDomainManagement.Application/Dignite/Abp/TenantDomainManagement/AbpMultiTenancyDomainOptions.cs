using System;
using JetBrains.Annotations;

namespace Dignite.Abp.TenantDomainManagement;
public class AbpMultiTenancyDomainOptions
{

    /// <summary>
    /// Tenant's second-level domain name format, e.g. “{0}.travely.dignite.com”
    /// </summary>
    public string TenantDomainFormat { get; set; }

    public string WebServerSiteName { get; set; } = "default";

    /// <summary>
    /// 
    /// </summary>
    public string UpstreamAddress { get; set; } = "https://localhost:5000";

    public string AuthServerClientId { get; set; }

    /// <summary>
    /// Get the full second-level domain name for a given tenant
    /// </summary>
    /// <param name="tenantName">tenant name</param>
    /// <returns>Full tenant domain name</returns>
    public string GetTenantDomain([NotNull] string tenantName)
    {
        if (string.IsNullOrWhiteSpace(TenantDomainFormat))
        {
            throw new InvalidOperationException("TenantDomainFormat cannot be empty, please check the configuration.");
        }
        if (!TenantDomainFormat.Contains("{0}"))
        {
            throw new InvalidOperationException("TenantDomainFormat must contain '{0}' as a placeholder.");
        }

        return string.Format(TenantDomainFormat, tenantName);
    }
}
