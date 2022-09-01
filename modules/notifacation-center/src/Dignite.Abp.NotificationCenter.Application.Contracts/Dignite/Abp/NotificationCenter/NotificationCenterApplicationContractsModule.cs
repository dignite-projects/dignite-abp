using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Dignite.Abp.NotificationCenter
{
    [DependsOn(
        typeof(NotificationCenterDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class NotificationCenterApplicationContractsModule : AbpModule
    {

    }
}
