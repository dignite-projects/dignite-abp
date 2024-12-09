using Dignite.FileExplorer;
using Volo.Abp.Modularity;
using Volo.CmsKit.Admin;

namespace Dignite.CmsKit.Admin;

[DependsOn(
    typeof(DigniteCmsKitDomainSharedModule),
    typeof(CmsKitAdminApplicationContractsModule),
    typeof(DigniteCmsKitCommonApplicationContractsModule),
    typeof(FileExplorerApplicationContractsModule)
    )]
public class DigniteCmsKitAdminApplicationContractsModule : AbpModule
{

}
