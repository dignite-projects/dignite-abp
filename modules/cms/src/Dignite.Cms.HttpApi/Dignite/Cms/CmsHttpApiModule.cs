using Dignite.Cms.Admin;
using Dignite.Cms.Public;
using Volo.CmsKit;
using Volo.Abp.Modularity;

namespace Dignite.Cms;

[DependsOn(
    typeof(CmsApplicationContractsModule),
    typeof(CmsAdminHttpApiModule),
    typeof(CmsPublicHttpApiModule),
    typeof(CmsKitHttpApiModule))]
public class CmsHttpApiModule : AbpModule
{
}
