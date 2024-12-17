using Volo.Abp.Modularity;

namespace Dignite.CmsKit;

[DependsOn(
    typeof(DigniteCmsKitApplicationModule),
    typeof(CmsKitDomainTestModule)
    )]
public class CmsKitApplicationTestModule : AbpModule
{

}
