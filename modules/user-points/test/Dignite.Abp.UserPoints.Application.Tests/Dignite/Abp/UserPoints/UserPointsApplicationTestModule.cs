using Volo.Abp.Modularity;

namespace Dignite.Abp.UserPoints;

[DependsOn(
    typeof(UserPointsApplicationModule),
    typeof(UserPointsDomainTestModule)
    )]
public class UserPointsApplicationTestModule : AbpModule
{

}
