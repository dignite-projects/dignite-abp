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


        builder.Entity<UserPointAccount>(b =>
        {
            //Configure table & schema name
            b.ToTable(UserPointsDbProperties.DbTablePrefix + "Accounts", UserPointsDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(up => up.PointTypeName).IsRequired().HasMaxLength(UserPointAccountConsts.MaxPointTypeNameLength);

            //Indexes
            b.HasIndex(up => new { up.UserId });

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<UserPointTransaction>(b =>
        {
            //Configure table & schema name
            b.ToTable(UserPointsDbProperties.DbTablePrefix + "Transactions", UserPointsDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(up => up.EntityType).HasMaxLength(UserPointTransactionConsts.MaxEntityTypeLength);
            b.Property(up => up.EntityId).HasMaxLength(UserPointTransactionConsts.MaxEntityIdLength);
            b.Property(up => up.Remark).HasMaxLength(UserPointTransactionConsts.MaxRemarkLength);
            b.Property(up => up.ExpirationDate).HasColumnType("date");

            //Indexes
            b.HasIndex(up => new { up.UserId, up.AccountId, up.ExpirationDate, up.ConsumptionPriority, up.CreationTime, up.AvailableAmount});
            b.HasIndex(up => new { up.UserId, up.AccountId, up.ExpirationDate, up.CreationTime, up.Amount, up.EntityType, up.EntityId });

            b.ApplyObjectExtensionMappings();
        });
    }
}
