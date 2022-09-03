
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.Files.MongoDB;

[DependsOn(
    typeof(AbpFilesDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class AbpFilesMongoDbModule : AbpModule
{
}
