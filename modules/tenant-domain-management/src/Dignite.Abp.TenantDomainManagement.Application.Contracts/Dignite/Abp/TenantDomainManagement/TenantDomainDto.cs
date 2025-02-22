using System;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.TenantDomainManagement;

public class TenantDomainDto : IMultiTenant
{
    public TenantDomainDto()
    {
    }

    public TenantDomainDto(string domainName, bool isValid, Guid? tenantId)
    {
        DomainName = domainName;
        IsValid = isValid;
        TenantId = tenantId;
    }

    public string DomainName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool IsValid { get; set; }

    public Guid? TenantId { get; set; }
}
