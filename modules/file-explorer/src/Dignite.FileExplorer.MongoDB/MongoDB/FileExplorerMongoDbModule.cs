using Dignite.Abp.Files.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Dignite.FileExplorer.MongoDB;

[DependsOn(
    typeof(FileExplorerDomainModule),
    typeof(AbpFilesMongoDbModule)
    )]
public class FileExplorerMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<FileExplorerMongoDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
        });
    }
}
