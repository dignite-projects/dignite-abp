using Volo.Abp.Modularity;

namespace Dignite.Abp.NotificationCenter;

[DependsOn(
    typeof(DigniteAbpNotificationCenterApplicationModule),
    typeof(DigniteAbpNotificationCenterDomainTestModule)
    )]
public class DigniteAbpNotificationCenterApplicationTestModule : AbpModule
{
}