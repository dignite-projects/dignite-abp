using Dignite.CmsKit.Favourites;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.CmsKit;
using Volo.CmsKit.EntityFrameworkCore;

namespace Dignite.CmsKit.EntityFrameworkCore;

[ConnectionStringName(AbpCmsKitDbProperties.ConnectionStringName)]
public class CmsKitDbContext : AbpDbContext<CmsKitDbContext>, ICmsKitDbContext
{
    public DbSet<Favourite> Favourites { get; set; }

    public CmsKitDbContext(DbContextOptions<CmsKitDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureCmsKit();
        builder.ConfigureDigniteCmsKit();
    }
}
