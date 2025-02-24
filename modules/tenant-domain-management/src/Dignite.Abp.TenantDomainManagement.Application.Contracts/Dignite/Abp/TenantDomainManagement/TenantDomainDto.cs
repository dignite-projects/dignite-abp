using System;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.TenantDomainManagement;

public class TenantDomainDto : IMultiTenant
{
    public TenantDomainDto()
    {
    }

    public TenantDomainDto(string domainName, bool isValid, string proxyAddress, Guid? tenantId)
    {
        DomainName = domainName;
        IsValid = isValid;
        ProxyAddress = proxyAddress;
        TenantId = tenantId;
    }

    public string DomainName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ProxyAddress { get; set; }

    public Guid? TenantId { get; set; }
}
