using System;
using JetBrains.Annotations;

namespace Dignite.Abp.TenantDomainManagement;
public class AbpTenantDomainManagementOptions
{
    /// <summary>
    /// Tenant's second-level domain name format, e.g. “{0}.travely.dignite.com”
    /// Used to verify that the tenant domain resolves cname to the tenant's second-level domain name
    /// </summary>
    public string TenantDomainFormat { get; set; }

    /// <summary>
    /// Specify the proxy address of the reverse proxy for the tenant's domain name
    /// </summary>
    public string ProxyAddress { get; set; } = "https://localhost:5000";

    /// <summary>
    /// Specify the client ID of the authorization service used to add the tenant domain to the list of redirect URLs
    /// </summary>
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
