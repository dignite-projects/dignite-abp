using Dignite.Publisher.Admin;
using Dignite.Publisher.Public;
using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace Dignite.Publisher;

[DependsOn(
    typeof(CmsKitApplicationModule),
    typeof(PublisherAdminApplicationModule),
    typeof(PublisherPublicApplicationModule),
    typeof(PublisherApplicationContractsModule)
    )]
public class PublisherApplicationModule : AbpModule
{
}
