using Dignite.Abp.Notifications;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Abp.NotificationCenter;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpNotificationsModule),
    typeof(AbpNotificationCenterDomainSharedModule)
)]
public class AbpNotificationCenterDomainModule : AbpModule
{

}
