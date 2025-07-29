using Dignite.Publisher.Admin;
using Dignite.Publisher.Public;
using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace Dignite.Publisher;

[DependsOn(
    typeof(CmsKitHttpApiModule),
    typeof(PublisherApplicationContractsModule),
    typeof(PublisherAdminHttpApiModule),
    typeof(PublisherPublicHttpApiModule))]
public class PublisherHttpApiModule : AbpModule
{
}
