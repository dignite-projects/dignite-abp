using Dignite.Abp.Notifications;
using Volo.Abp.Domain;
using Volo.Abp.Json.Newtonsoft;
using Volo.Abp.Modularity;

namespace Dignite.Abp.NotificationCenter;

[DependsOn(
    typeof(AbpJsonNewtonsoftModule),
    typeof(AbpDddDomainModule),
    typeof(DigniteAbpNotificationsModule),
    typeof(DigniteAbpNotificationCenterDomainSharedModule)
)]
public class DigniteAbpNotificationCenterDomainModule : AbpModule
{
}