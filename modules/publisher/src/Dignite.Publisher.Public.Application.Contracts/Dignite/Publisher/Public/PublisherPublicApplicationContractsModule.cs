
using Volo.Abp.Modularity;
using Volo.CmsKit.Public;

namespace Dignite.Publisher.Public;

[DependsOn(
    typeof(CmsKitPublicApplicationContractsModule),
    typeof(PublisherCommonApplicationContractsModule)
    )]
public class PublisherPublicApplicationContractsModule : AbpModule
{

}
