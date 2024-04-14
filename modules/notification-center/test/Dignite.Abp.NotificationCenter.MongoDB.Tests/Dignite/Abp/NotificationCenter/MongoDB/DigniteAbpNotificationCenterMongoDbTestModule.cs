using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace Dignite.Abp.NotificationCenter.MongoDB;

[DependsOn(
    typeof(DigniteAbpNotificationCenterTestBaseModule),
    typeof(DigniteAbpNotificationCenterMongoDbModule)
    )]
public class DigniteAbpNotificationCenterMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
