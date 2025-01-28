using Dignite.Cms.Admin;
using Dignite.Cms.Public;
using Volo.CmsKit;
using Volo.Abp.Modularity;

namespace Dignite.Cms;

[DependsOn(
    typeof(CmsAdminApplicationContractsModule),
    typeof(CmsPublicApplicationContractsModule),
    typeof(CmsKitApplicationContractsModule)
    )]
public class CmsApplicationContractsModule : AbpModule
{

}
