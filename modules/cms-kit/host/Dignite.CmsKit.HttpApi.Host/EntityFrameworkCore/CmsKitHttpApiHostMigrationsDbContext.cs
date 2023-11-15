using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.CmsKit.EntityFrameworkCore;

public class CmsKitHttpApiHostMigrationsDbContext : AbpDbContext<CmsKitHttpApiHostMigrationsDbContext>
{
    public CmsKitHttpApiHostMigrationsDbContext(DbContextOptions<CmsKitHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureDigniteCmsKit();
    }
}
