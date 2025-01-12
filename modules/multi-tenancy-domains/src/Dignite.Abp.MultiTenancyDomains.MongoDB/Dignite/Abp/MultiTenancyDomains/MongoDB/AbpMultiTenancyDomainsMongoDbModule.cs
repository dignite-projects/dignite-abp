using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.MultiTenancyDomains.MongoDB;

[DependsOn(
    typeof(AbpMultiTenancyDomainsDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class AbpMultiTenancyDomainsMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<MultiTenancyDomainsMongoDbContext>(options =>
        {
            options.AddRepository<TenantDomain, MongoTenantDomainRepository>();
        });
    }
}
