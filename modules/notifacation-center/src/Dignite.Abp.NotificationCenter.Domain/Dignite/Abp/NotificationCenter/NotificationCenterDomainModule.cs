using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Abp.NotificationCenter
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(NotificationCenterDomainSharedModule)
    )]
    public class NotificationCenterDomainModule : AbpModule
    {

    }
}
