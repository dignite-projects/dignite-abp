using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Abp.UserPoints;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(UserPointsDomainSharedModule)
)]
public class UserPointsDomainModule : AbpModule
{

}
