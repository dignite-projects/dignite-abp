using Volo.CmsKit;
using Volo.Abp.Modularity;
using Dignite.FileExplorer;

namespace Dignite.Cms;

[DependsOn(
    typeof(CmsDomainSharedModule),
    typeof(CmsKitDomainModule),
    typeof(FileExplorerDomainModule)
)]
public class CmsDomainModule : AbpModule
{
}
