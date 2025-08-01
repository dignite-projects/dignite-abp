using Dignite.FileExplorer;
using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace Dignite.Publisher;

[DependsOn(
    typeof(CmsKitDomainModule),
    typeof(FileExplorerDomainModule),
    typeof(PublisherDomainSharedModule)
)]
public class PublisherDomainModule : AbpModule
{

}
