using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Dignite.Abp.TenantDomains.MongoDB;

[DependsOn(
    typeof(AbpTenantDomainsApplicationTestModule),
    typeof(AbpTenantDomainsMongoDbModule)
)]
public class AbpTenantDomainsMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
