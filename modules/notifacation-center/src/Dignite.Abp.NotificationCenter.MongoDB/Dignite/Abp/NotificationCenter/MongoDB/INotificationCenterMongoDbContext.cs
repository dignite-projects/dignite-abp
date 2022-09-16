using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.NotificationCenter.MongoDB;

[ConnectionStringName(NotificationCenterDbProperties.ConnectionStringName)]
public interface INotificationCenterMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<Notification> Notifications { get; }
    IMongoCollection<UserNotification> UserNotifications { get; }
    IMongoCollection<NotificationSubscription> NotificationSubscriptions { get; }
}
