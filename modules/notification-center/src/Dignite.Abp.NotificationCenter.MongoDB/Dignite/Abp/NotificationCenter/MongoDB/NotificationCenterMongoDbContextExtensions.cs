using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.NotificationCenter.MongoDB;

public static class NotificationCenterMongoDbContextExtensions
{
    public static void ConfigureNotificationCenter(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<NotificationSubscription>(x =>
        {
            x.CollectionName = NotificationCenterDbProperties.DbTablePrefix + "NotificationSubscriptions";
        });

        builder.Entity<Notification>(x =>
        {
            x.CollectionName = NotificationCenterDbProperties.DbTablePrefix + "Notifications";
        });

        builder.Entity<UserNotification>(x =>
        {
            x.CollectionName = NotificationCenterDbProperties.DbTablePrefix + "UserNotifications";
        });
    }
}
