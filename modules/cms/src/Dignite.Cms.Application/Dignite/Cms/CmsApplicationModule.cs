using Dignite.Cms.Admin;
using Dignite.Cms.Public;
using Volo.CmsKit;
using Volo.Abp.Modularity;

namespace Dignite.Cms;

[DependsOn(
    typeof(CmsApplicationContractsModule),
    typeof(CmsPublicApplicationModule),
    typeof(CmsAdminApplicationModule),
    typeof(CmsKitApplicationModule)
    )]
public class CmsApplicationModule : AbpModule
{
}
