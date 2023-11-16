using Volo.Abp.Modularity;
using Volo.CmsKit.Public;

namespace Dignite.CmsKit.Public;

[DependsOn(
    typeof(DigniteCmsKitDomainSharedModule),
    typeof(CmsKitPublicApplicationContractsModule)
    )]
public class DigniteCmsKitPublicApplicationContractsModule : AbpModule
{

}
