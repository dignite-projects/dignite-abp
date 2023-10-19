using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.UserPoints.EntityFrameworkCore;

public class UserPointsHttpApiHostMigrationsDbContext : AbpDbContext<UserPointsHttpApiHostMigrationsDbContext>
{
    public UserPointsHttpApiHostMigrationsDbContext(DbContextOptions<UserPointsHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureUserPoints();
    }
}
