using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.MultiTenancyDomains;

public class TenantDomainDto : EntityDto<Guid>, IMultiTenant
{
    public string DomainName { get; set; }

    public Guid? TenantId { get; set; }
}
