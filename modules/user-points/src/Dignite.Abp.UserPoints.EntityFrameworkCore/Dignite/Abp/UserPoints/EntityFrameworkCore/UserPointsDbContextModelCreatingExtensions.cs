using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Dignite.Abp.UserPoints.EntityFrameworkCore;

public static class UserPointsDbContextModelCreatingExtensions
{
    public static void ConfigureUserPoints(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));


        builder.Entity<UserPoint>(b =>
        {
            //Configure table & schema name
            b.ToTable(UserPointsDbProperties.DbTablePrefix + "UserPoints", UserPointsDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(up => up.PointType).HasMaxLength(UserPointConsts.MaxPointTypeLength);
            b.Property(up => up.EntityType).HasMaxLength(UserPointConsts.MaxEntityTypeLength);
            b.Property(up => up.EntityId).HasMaxLength(UserPointConsts.MaxEntityIdLength);

            //Indexes
            b.HasIndex(up => new { up.UserId, up.CreationTime, up.PointType, up.Amount, up.ExpirationTime, up.EntityType, up.EntityId });

            b.ApplyObjectExtensionMappings();
        });
    }
}
