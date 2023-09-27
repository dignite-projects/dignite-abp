using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.NotificationCenter.MongoDB;

[DependsOn(
    typeof(DigniteAbpNotificationCenterDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class DigniteAbpNotificationCenterMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<NotificationCenterMongoDbContext>(options =>
        {
            options.AddRepository<NotificationSubscription, MongoNotificationSubscriptionRepository>();
            options.AddRepository<Notification, MongoNotificationRepository>();
            options.AddRepository<UserNotification, MongoUserNotificationRepository>();
        });
    }
}
