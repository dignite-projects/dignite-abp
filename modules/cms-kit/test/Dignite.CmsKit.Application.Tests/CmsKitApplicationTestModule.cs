using Dignite.CmsKit.Public;
using Volo.Abp.Modularity;

namespace Dignite.CmsKit;

[DependsOn(
    typeof(DigniteCmsKitPublicApplicationModule),
    typeof(CmsKitDomainTestModule)
    )]
public class CmsKitApplicationTestModule : AbpModule
{

}
