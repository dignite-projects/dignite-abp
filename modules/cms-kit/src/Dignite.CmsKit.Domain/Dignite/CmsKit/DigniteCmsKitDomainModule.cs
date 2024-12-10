using Dignite.FileExplorer;
using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace Dignite.CmsKit;

[DependsOn(
    typeof(CmsKitDomainModule),
    typeof(DigniteCmsKitDomainSharedModule),
    typeof(FileExplorerDomainModule)
)]
public class DigniteCmsKitDomainModule : AbpModule
{
}
