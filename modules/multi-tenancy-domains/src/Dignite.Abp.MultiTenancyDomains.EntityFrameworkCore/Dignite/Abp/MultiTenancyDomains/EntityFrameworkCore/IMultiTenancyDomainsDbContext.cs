using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.MultiTenancyDomains.EntityFrameworkCore;

[ConnectionStringName(MultiTenancyDomainsDbProperties.ConnectionStringName)]
public interface IMultiTenancyDomainsDbContext : IEfCoreDbContext
{
    DbSet<TenantDomain> Domains { get; }
}
