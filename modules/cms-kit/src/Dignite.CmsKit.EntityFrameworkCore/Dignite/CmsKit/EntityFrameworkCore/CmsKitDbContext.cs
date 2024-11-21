using Dignite.CmsKit.Visits;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.CmsKit;

namespace Dignite.CmsKit.EntityFrameworkCore;

[ConnectionStringName(AbpCmsKitDbProperties.ConnectionStringName)]
public class CmsKitDbContext : AbpDbContext<CmsKitDbContext>, ICmsKitDbContext
{
    public DbSet<Visit> Visits { get; set; }

    public CmsKitDbContext(DbContextOptions<CmsKitDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDigniteCmsKit();
    }
}
