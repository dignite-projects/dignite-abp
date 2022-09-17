using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.NotificationCenter.MongoDB;

[ConnectionStringName(NotificationCenterDbProperties.ConnectionStringName)]
public class NotificationCenterMongoDbContext : AbpMongoDbContext, INotificationCenterMongoDbContext
{
    public IMongoCollection<Notification> Notifications => Collection<Notification>();
    public IMongoCollection<UserNotification> UserNotifications => Collection<UserNotification>();
    public IMongoCollection<NotificationSubscription> NotificationSubscriptions => Collection<NotificationSubscription>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureNotificationCenter();
    }
}
