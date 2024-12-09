using Dignite.CmsKit.Admin;
using Dignite.CmsKit.Public;
using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace Dignite.CmsKit;

[DependsOn(
    typeof(DigniteCmsKitDomainSharedModule),
    typeof(DigniteCmsKitPublicApplicationContractsModule),
    typeof(DigniteCmsKitAdminApplicationContractsModule),
    typeof(CmsKitApplicationContractsModule)
    )]
public class DigniteCmsKitApplicationContractsModule : AbpModule
{

}
