using Volo.Abp.Modularity;

namespace Dignite.Abp.RegionalizationManagement;

[DependsOn(
    typeof(AbpRegionalizationManagementApplicationModule),
    typeof(AbpRegionalizationManagementTestBaseModule)
    )]
public class AbpRegionalizationManagementApplicationTestModule : AbpModule
{

}
