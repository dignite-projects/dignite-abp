using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using static System.Collections.Specialized.BitVector32;

namespace Dignite.Abp.UserPoints.EntityFrameworkCore;

public static class UserPointsDbContextModelCreatingExtensions
{
    public static void ConfigureUserPoints(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Ignore<UserPointsOrderRedeem>();

        builder.Entity<UserPointsItem>(b =>
        {
            //Configure table & schema name
            b.ToTable(UserPointsDbProperties.DbTablePrefix + "UserPointsItems", UserPointsDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(i => i.PointsDefinitionName).IsRequired().HasMaxLength(UserPointsItemConsts.MaxPointsDefinitionNameLength);
            b.Property(i => i.PointsWorkflowName).IsRequired().HasMaxLength(UserPointsItemConsts.MaxPointsWorkflowNameLength);

            //Relations
            b.HasMany(i => i.PointsBlocks).WithOne(i=>i.UserPointsItem).HasForeignKey(b => b.UserPointsItemId);

            //Indexes
            b.HasIndex(i => new { i.UserId,i.PointsType,i.PointsDefinitionName,i.PointsWorkflowName,i.CreationTime, i.ExpirationDate });

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<UserPointsOrder>(b =>
        {
            //Configure table & schema name
            b.ToTable(UserPointsDbProperties.DbTablePrefix + "UserPointsOrders", UserPointsDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(o => o.BusinessOrderType).IsRequired().HasMaxLength(UserPointsOrderConsts.MaxBusinessOrderTypeLength);
            b.Property(o => o.BusinessOrderNumber).IsRequired().HasMaxLength(UserPointsOrderConsts.MaxBusinessOrderNumberLength);
            b.Property(s => s.Redeems).IsRequired().HasConversion(
                config => JsonSerializer.Serialize(config, new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                }),
                jsonData => JsonSerializer.Deserialize<ICollection<UserPointsOrderRedeem>>(jsonData, new JsonSerializerOptions())
            );


            //Indexes
            b.HasIndex(o => new { o.BusinessOrderType, o.BusinessOrderNumber });
            b.HasIndex(o => new { o.UserId, o.CreationTime });

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<UserPointsBlock>(b =>
        {
            //Configure table & schema name
            b.ToTable(UserPointsDbProperties.DbTablePrefix + "UserPointsBlocks", UserPointsDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Relations
            b.HasOne(s => s.UserPointsItem).WithMany(i=>i.PointsBlocks).HasForeignKey(s => s.UserPointsItemId);

            //Indexes
            b.HasIndex(b => b.UserPointsItemId);

            b.ApplyObjectExtensionMappings();
        });
    }
}
