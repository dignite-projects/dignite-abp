using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.Files.MongoDB;

[DependsOn(
    typeof(DigniteAbpFilesDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class DigniteAbpFilesMongoDbModule : AbpModule
{
}