using Dignite.Abp.RegionalizationManagement;
using Volo.CmsKit.Admin;
using Volo.Abp.Modularity;
using Dignite.FileExplorer;

namespace Dignite.Cms.Admin;

[DependsOn(
    typeof(CmsCommonApplicationContractsModule),
    typeof(CmsKitAdminApplicationContractsModule),
    typeof(AbpRegionalizationManagementApplicationContractsModule),
    typeof(FileExplorerApplicationContractsModule)
    )]
public class CmsAdminApplicationContractsModule : AbpModule
{

}
