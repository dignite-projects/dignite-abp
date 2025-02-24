namespace Dignite.Abp.TenantDomain;
public static class WebServerConsts
{
    /// <summary>
    /// Used in the reverse proxy process to store the tenant ID corresponding to the domain name
    /// </summary>
    public static string ProxyHeaderTenantId = "Abp-Forwarded-Tenant-Id";
}
