using Dignite.Abp.Files;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.FileExplorer;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(FileExplorerDomainSharedModule),
    typeof(DigniteAbpFilesDomainModule)
)]
public class FileExplorerDomainModule : AbpModule
{
}