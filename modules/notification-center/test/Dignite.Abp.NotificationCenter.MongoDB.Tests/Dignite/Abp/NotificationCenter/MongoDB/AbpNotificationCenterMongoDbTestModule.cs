using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace Dignite.Abp.NotificationCenter.MongoDB;

[DependsOn(
    typeof(AbpNotificationCenterTestBaseModule),
    typeof(AbpNotificationCenterMongoDbModule)
    )]
public class AbpNotificationCenterMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
