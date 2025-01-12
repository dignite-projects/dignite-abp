using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Dignite.Abp.MultiTenancyDomains.MongoDB;

[DependsOn(
    typeof(AbpMultiTenancyDomainsApplicationTestModule),
    typeof(AbpMultiTenancyDomainsMongoDbModule)
)]
public class AbpMultiTenancyDomainsMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
