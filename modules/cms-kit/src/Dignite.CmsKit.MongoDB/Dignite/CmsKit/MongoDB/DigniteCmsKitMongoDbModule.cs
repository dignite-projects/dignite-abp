using Dignite.CmsKit.MongoDB.Visits;
using Dignite.CmsKit.Visits;
using Dignite.FileExplorer.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.CmsKit.MongoDB;

namespace Dignite.CmsKit.MongoDB;

[DependsOn(
    typeof(DigniteCmsKitDomainModule),
    typeof(CmsKitMongoDbModule),
    typeof(FileExplorerMongoDbModule)
    )]
public class DigniteCmsKitMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<CmsKitMongoDbContext>(options =>
        {
            options.AddRepository<Visit, MongoVisitRepository>();
        });
    }
}
