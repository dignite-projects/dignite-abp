using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace Dignite.FileExplorer.MongoDB;

[DependsOn(
    typeof(FileExplorerTestBaseModule),
    typeof(FileExplorerMongoDbModule)
    )]
public class FileExplorerMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
