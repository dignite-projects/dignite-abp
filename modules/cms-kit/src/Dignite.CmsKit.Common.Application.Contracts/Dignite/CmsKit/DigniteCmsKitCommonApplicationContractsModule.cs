using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace Dignite.CmsKit;

[DependsOn(
    typeof(DigniteCmsKitDomainSharedModule),
    typeof(CmsKitCommonApplicationContractsModule)
    )]
public class DigniteCmsKitCommonApplicationContractsModule : AbpModule
{

}
