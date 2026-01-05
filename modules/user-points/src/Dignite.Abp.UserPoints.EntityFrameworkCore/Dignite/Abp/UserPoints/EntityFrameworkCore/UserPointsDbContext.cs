using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.UserPoints.EntityFrameworkCore;

[ConnectionStringName(UserPointsDbProperties.ConnectionStringName)]
public class UserPointsDbContext : AbpDbContext<UserPointsDbContext>, IUserPointsDbContext
{
    public DbSet<UserPointAccount> Accounts { get; set; }
    public DbSet<UserPointTransaction> Transactions { get; set; }

    public UserPointsDbContext(DbContextOptions<UserPointsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureUserPoints();
    }
}
