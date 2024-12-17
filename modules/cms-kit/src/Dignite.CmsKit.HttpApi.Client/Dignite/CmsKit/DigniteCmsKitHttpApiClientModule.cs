using Dignite.CmsKit.Public;
using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace Dignite.CmsKit;

[DependsOn(
    typeof(DigniteCmsKitApplicationContractsModule),
    typeof(DigniteCmsKitPublicHttpApiClientModule),
    typeof(CmsKitHttpApiClientModule)
    )]
public class DigniteCmsKitHttpApiClientModule : AbpModule
{
}
