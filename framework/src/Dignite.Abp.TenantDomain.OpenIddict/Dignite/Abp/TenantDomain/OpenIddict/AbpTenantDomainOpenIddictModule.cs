using Dignite.Abp.TenantDomain.WebServer;
using Volo.Abp.Modularity;

namespace Dignite.Abp.TenantDomain.OpenIddict;

[DependsOn(typeof(AbpTenantDomainModule))]
public class AbpTenantDomainOpenIddictModule:AbpModule
{
}
