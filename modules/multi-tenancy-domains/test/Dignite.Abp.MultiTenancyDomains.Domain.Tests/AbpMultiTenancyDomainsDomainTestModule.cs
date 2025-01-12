using Volo.Abp.Modularity;

namespace Dignite.Abp.MultiTenancyDomains;

[DependsOn(
    typeof(AbpMultiTenancyDomainsDomainModule),
    typeof(AbpMultiTenancyDomainsTestBaseModule)
)]
public class AbpMultiTenancyDomainsDomainTestModule : AbpModule
{

}
