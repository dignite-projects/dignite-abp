using Volo.Abp.Modularity;

namespace Dignite.Abp.MultiTenancyDomains;

[DependsOn(
    typeof(AbpMultiTenancyDomainsApplicationModule),
    typeof(AbpMultiTenancyDomainsDomainTestModule)
    )]
public class AbpMultiTenancyDomainsApplicationTestModule : AbpModule
{

}
