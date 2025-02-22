using Dignite.Abp.TenantDomain.WebServer;
using Volo.Abp.Modularity;

namespace Dignite.Abp.TenantDomain.Caddy;

[DependsOn(typeof(AbpTenantDomainModule))]
public class AbpTenantDomainCaddyModule : AbpModule
{
}
