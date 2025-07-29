
using Volo.Abp.Modularity;
using Volo.CmsKit.Admin;

namespace Dignite.Publisher.Admin;

[DependsOn(
    typeof(CmsKitAdminApplicationContractsModule),
    typeof(PublisherCommonApplicationContractsModule)
    )]
public class PublisherAdminApplicationContractsModule : AbpModule
{

}
