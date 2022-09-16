using Dignite.Abp.Files.MongoDB;
using Dignite.FileExplorer.Files;
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
            options.AddRepository<FileDescriptor, MongoFileDescriptorRepository>();
        });
    }
}