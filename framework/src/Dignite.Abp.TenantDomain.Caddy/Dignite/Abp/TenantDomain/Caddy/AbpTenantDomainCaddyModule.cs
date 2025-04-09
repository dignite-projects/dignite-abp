using Dignite.Abp.TenantDomain.WebServer;
using Volo.Abp.Modularity;

namespace Dignite.Abp.TenantDomain.Caddy;

[DependsOn(typeof(AbpTenantDomainModule), typeof(DigniteCaddyModule))]
public class AbpTenantDomainCaddyModule : AbpModule
{
}
