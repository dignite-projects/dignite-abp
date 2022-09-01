using Volo.Abp.Modularity;

namespace Dignite.Abp.NotificationCenter
{
    [DependsOn(
        typeof(NotificationCenterApplicationModule),
        typeof(NotificationCenterDomainTestModule)
        )]
    public class NotificationCenterApplicationTestModule : AbpModule
    {

    }
}
