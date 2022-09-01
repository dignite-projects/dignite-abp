using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.NotificationCenter.EntityFrameworkCore
{
    [ConnectionStringName(NotificationCenterDbProperties.ConnectionStringName)]
    public interface INotificationCenterDbContext : IEfCoreDbContext
    {
        DbSet<Notification> Notifications { get; }
        DbSet<UserNotification> UserNotifications { get; }
        DbSet<NotificationSubscription> NotificationSubscriptions { get; }
    }
}