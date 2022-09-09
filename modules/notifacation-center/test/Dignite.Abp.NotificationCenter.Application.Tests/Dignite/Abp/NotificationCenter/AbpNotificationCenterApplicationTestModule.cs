using Volo.Abp.Modularity;

namespace Dignite.Abp.NotificationCenter;

[DependsOn(
    typeof(AbpNotificationCenterApplicationModule),
    typeof(AbpNotificationCenterDomainTestModule)
    )]
public class AbpNotificationCenterApplicationTestModule : AbpModule
{

}
