using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Dignite.CmsKit.MongoDB;

[DependsOn(
    typeof(CmsKitTestBaseModule),
    typeof(DigniteCmsKitMongoDbModule)
    )]
public class CmsKitMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
