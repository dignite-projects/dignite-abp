using Dignite.Abp.Points;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Abp.UserPoints;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(UserPointsDomainSharedModule),
    typeof(DigniteAbpPointsModule)
)]
public class UserPointsDomainModule : AbpModule
{

}
