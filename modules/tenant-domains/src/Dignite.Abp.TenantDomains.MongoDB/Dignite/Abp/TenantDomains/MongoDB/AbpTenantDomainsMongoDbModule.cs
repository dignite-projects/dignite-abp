using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.TenantDomains.MongoDB;

[DependsOn(
    typeof(AbpTenantDomainsDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class AbpTenantDomainsMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<TenantDomainsMongoDbContext>(options =>
        {
            options.AddRepository<TenantDomain, MongoTenantDomainRepository>();
        });
    }
}
