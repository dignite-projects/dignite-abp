using Dignite.Abp.Notifications;
using Dignite.Abp.Notifications.Identity;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Abp.NotificationCenter;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpNotificationsIdentityModule),
    typeof(AbpNotificationCenterDomainSharedModule)
)]
public class AbpNotificationCenterDomainModule : AbpModule
{
}