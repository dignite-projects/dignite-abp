using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.MultiTenancyDomains.EntityFrameworkCore;

[ConnectionStringName(MultiTenancyDomainsDbProperties.ConnectionStringName)]
public class MultiTenancyDomainsDbContext : AbpDbContext<MultiTenancyDomainsDbContext>, IMultiTenancyDomainsDbContext
{
    public DbSet<TenantDomain> Domains { get; set; }

    public MultiTenancyDomainsDbContext(DbContextOptions<MultiTenancyDomainsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureTenantDomains();
    }
}
