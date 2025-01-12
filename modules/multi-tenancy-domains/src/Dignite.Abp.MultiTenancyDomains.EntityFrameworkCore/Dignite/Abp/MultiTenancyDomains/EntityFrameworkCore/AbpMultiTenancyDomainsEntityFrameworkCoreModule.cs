using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dignite.Abp.MultiTenancyDomains.EntityFrameworkCore;

[DependsOn(
    typeof(AbpMultiTenancyDomainsDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class AbpMultiTenancyDomainsEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<MultiTenancyDomainsDbContext>(options =>
        {
            options.AddRepository<TenantDomain, EfCoreTenantDomainRepository>();
        });
    }
}
