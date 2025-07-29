using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace Dignite.Publisher;

[DependsOn(
    typeof(CmsKitDomainModule),
    typeof(PublisherDomainSharedModule)
)]
public class PublisherDomainModule : AbpModule
{

}
