using Volo.Abp.Modularity;

namespace Dignite.Abp.RegionalizationManagement;

[DependsOn(
    typeof(RegionalizationManagementApplicationModule),
    typeof(RegionalizationManagementTestBaseModule)
    )]
public class RegionalizationManagementApplicationTestModule : AbpModule
{

}
