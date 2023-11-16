using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace Dignite.CmsKit;

[DependsOn(
    typeof(CmsKitDomainModule),
    typeof(DigniteCmsKitDomainSharedModule)
)]
public class DigniteCmsKitDomainModule : AbpModule
{

}
