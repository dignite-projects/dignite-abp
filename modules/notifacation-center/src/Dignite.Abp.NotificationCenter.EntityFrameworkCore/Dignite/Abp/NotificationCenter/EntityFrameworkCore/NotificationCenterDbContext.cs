using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.NotificationCenter.EntityFrameworkCore
{
    [ConnectionStringName(NotificationCenterDbProperties.ConnectionStringName)]
    public class NotificationCenterDbContext : AbpDbContext<NotificationCenterDbContext>, INotificationCenterDbContext
    {
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<NotificationSubscription> NotificationSubscriptions { get; set; }

        public NotificationCenterDbContext(DbContextOptions<NotificationCenterDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureNotificationCenter();
        }
    }
}