using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Cms.EntityFrameworkCore;

public class CmsHttpApiHostMigrationsDbContext : AbpDbContext<CmsHttpApiHostMigrationsDbContext>
{
    public CmsHttpApiHostMigrationsDbContext(DbContextOptions<CmsHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureCms();
    }
}
