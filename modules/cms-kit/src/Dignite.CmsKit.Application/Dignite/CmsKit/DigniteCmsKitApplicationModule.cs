using Dignite.CmsKit.Admin;
using Dignite.CmsKit.Public;
using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace Dignite.CmsKit;

[DependsOn(
    typeof(DigniteCmsKitApplicationContractsModule),
    typeof(DigniteCmsKitAdminApplicationModule),
    typeof(DigniteCmsKitPublicApplicationModule),
    typeof(CmsKitApplicationModule)
    )]
public class DigniteCmsKitApplicationModule : AbpModule
{
}
