using Dignite.Abp.TenantDomains;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.TenantDomains.EntityFrameworkCore;

[ConnectionStringName(TenantDomainsDbProperties.ConnectionStringName)]
public interface ITenantDomainsDbContext : IEfCoreDbContext
{
    DbSet<TenantDomain> Domains { get; }
}
