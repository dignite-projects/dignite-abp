using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace Dignite.Publisher;

[DependsOn(
    typeof(CmsKitCommonApplicationContractsModule),
    typeof(PublisherDomainSharedModule)
    )]
public class PublisherCommonApplicationContractsModule : AbpModule
{

}
