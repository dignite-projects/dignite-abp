using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dignite.Abp.TenantDomains.EntityFrameworkCore;

[DependsOn(
    typeof(AbpTenantDomainsDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class AbpTenantDomainsEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<TenantDomainsDbContext>(options =>
        {
            options.AddRepository<TenantDomain, EfCoreTenantDomainRepository>();
        });
    }
}
