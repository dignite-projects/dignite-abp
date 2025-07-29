using Dignite.Publisher.Admin;
using Dignite.Publisher.Public;
using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace Dignite.Publisher;

[DependsOn(
    typeof(CmsKitApplicationContractsModule),
    typeof(PublisherAdminApplicationContractsModule),
    typeof(PublisherPublicApplicationContractsModule)
    )]
public class PublisherApplicationContractsModule : AbpModule
{

}
