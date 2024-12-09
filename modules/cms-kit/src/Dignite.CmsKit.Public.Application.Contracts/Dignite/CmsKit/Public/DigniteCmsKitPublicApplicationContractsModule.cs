using Dignite.FileExplorer;
using Volo.Abp.Modularity;
using Volo.CmsKit.Public;

namespace Dignite.CmsKit.Public;

[DependsOn(
    typeof(DigniteCmsKitDomainSharedModule),
    typeof(DigniteCmsKitCommonApplicationContractsModule),
    typeof(CmsKitPublicApplicationContractsModule),
    typeof(FileExplorerApplicationContractsModule)
    )]
public class DigniteCmsKitPublicApplicationContractsModule : AbpModule
{

}
