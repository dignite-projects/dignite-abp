using Dignite.Abp.DynamicForms.FileExplorer;
using Volo.CmsKit.Public;
using Volo.Abp.Modularity;
using Dignite.FileExplorer;

namespace Dignite.Cms.Public;

[DependsOn(
    typeof(CmsCommonApplicationContractsModule),
    typeof(AbpDynamicFormsFileExplorerModule),
    typeof(CmsKitPublicApplicationContractsModule),
    typeof(FileExplorerApplicationContractsModule)
    )]
public class CmsPublicApplicationContractsModule : AbpModule
{

}
