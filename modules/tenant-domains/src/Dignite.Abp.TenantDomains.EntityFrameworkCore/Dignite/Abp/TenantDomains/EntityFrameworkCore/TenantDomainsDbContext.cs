using Dignite.Abp.TenantDomains;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.TenantDomains.EntityFrameworkCore;

[ConnectionStringName(TenantDomainsDbProperties.ConnectionStringName)]
public class TenantDomainsDbContext : AbpDbContext<TenantDomainsDbContext>, ITenantDomainsDbContext
{
    public DbSet<TenantDomain> Domains { get; set; }

    public TenantDomainsDbContext(DbContextOptions<TenantDomainsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureTenantDomains();
    }
}
