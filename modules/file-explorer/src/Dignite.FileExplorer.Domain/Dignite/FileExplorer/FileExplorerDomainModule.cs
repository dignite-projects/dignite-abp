using Dignite.Abp.Files;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.FileExplorer;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(FileExplorerDomainSharedModule),
    typeof(AbpFilesDomainModule)
)]
public class FileExplorerDomainModule : AbpModule
{
}