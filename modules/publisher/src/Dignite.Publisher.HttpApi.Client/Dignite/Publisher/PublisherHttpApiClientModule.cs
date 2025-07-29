using Dignite.Publisher.Admin;
using Dignite.Publisher.Public;
using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace Dignite.Publisher;

[DependsOn(
    typeof(CmsKitHttpApiClientModule),
    typeof(PublisherApplicationContractsModule),
    typeof(PublisherAdminHttpApiClientModule),
    typeof(PublisherPublicHttpApiClientModule))]
public class PublisherHttpApiClientModule : AbpModule
{
}
