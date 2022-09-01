using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Dignite.Abp.NotificationCenter.EntityFrameworkCore
{
    public static class NotificationCenterDbContextModelCreatingExtensions
    {
        public static void ConfigureNotificationCenter(
            this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));


            builder.Entity<NotificationSubscription>(b =>
            {
                //Configure table & schema name
                b.ToTable(NotificationCenterDbProperties.DbTablePrefix + "NotificationSubscriptions", NotificationCenterDbProperties.DbSchema);

                b.ConfigureByConvention();

                //Properties
                b.Property(ns => ns.NotificationName).IsRequired().HasMaxLength(NotificationConsts.MaxNotificationNameLength);
                b.Property(ns => ns.EntityTypeName).HasMaxLength(NotificationConsts.MaxEntityTypeNameLength);
                b.Property(ns => ns.EntityId).HasMaxLength(NotificationConsts.MaxEntityIdLength);

                //Keys
                b.HasKey(x => new { x.UserId, x.NotificationName,x.EntityTypeName,x.EntityId });

                //Indexes
                b.HasIndex(ns => new{
                    ns.CreationTime,
                    ns.UserId
                });
            });

            builder.Entity<Notification>(b =>
            {
                //Configure table & schema name
                b.ToTable(NotificationCenterDbProperties.DbTablePrefix + "Notifications", NotificationCenterDbProperties.DbSchema);

                b.ConfigureByConvention();

                //Properties
                b.Property(n => n.NotificationName).IsRequired().HasMaxLength(NotificationConsts.MaxNotificationNameLength);
                b.Property(n => n.EntityTypeName).HasMaxLength(NotificationConsts.MaxEntityTypeNameLength);
                b.Property(n => n.EntityId).HasMaxLength(NotificationConsts.MaxEntityIdLength);
                b.Property(n => n.Data).IsRequired().HasMaxLength(NotificationConsts.MaxDataLength);
                b.Property(n => n.DataTypeName).IsRequired().HasMaxLength(NotificationConsts.MaxDataTypeNameLength);

                //Indexes
                b.HasIndex(n => new {
                    n.CreationTime,
                    n.Id
                });
            });



            builder.Entity<UserNotification>(b =>
            {
                //Configure table & schema name
                b.ToTable(NotificationCenterDbProperties.DbTablePrefix + "UserNotifications", NotificationCenterDbProperties.DbSchema);

                b.ConfigureByConvention();


                //Relations
                b.HasOne(un => un.Notification).WithOne().IsRequired().HasForeignKey<UserNotification>(un => un.NotificationId);

                //Keys
                b.HasKey(x => new { x.UserId,x.NotificationId });

                //Indexes
                b.HasIndex(un => un.UserId);
            });
        }
    }
}
