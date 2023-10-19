using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Dignite.Abp.UserPoints;

[DependsOn(
    typeof(UserPointsDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class UserPointsApplicationContractsModule : AbpModule
{

}
