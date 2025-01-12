using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.MultiTenancyDomains;

public class TenantDomain : CreationAuditedAggregateRoot<Guid>, IMultiTenant
{
    public TenantDomain(Guid id, string domainName, Guid? tenantId)
        :base(id)
    {
        DomainName = domainName;
        TenantId = tenantId;
    }

    public string DomainName { get; set; }

    public Guid? TenantId { get; set; }
}
