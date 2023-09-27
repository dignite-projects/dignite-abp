using Volo.Abp.Authorization;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Notifications.Identity;

[DependsOn(
    typeof(AbpAuthorizationModule),
    typeof(DigniteAbpNotificationsModule),
    typeof(AbpIdentityDomainModule)
    )]
public class DigniteAbpNotificationsIdentityModule:AbpModule
{
}

