using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Dignite.CmsKit.Public;

[DependsOn(
    typeof(DigniteCmsKitDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class DigniteCmsKitPublicApplicationContractsModule : AbpModule
{

}
