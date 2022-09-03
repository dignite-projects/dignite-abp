using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.FileExplorer;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(FileExplorerDomainSharedModule)
)]
public class FileExplorerDomainModule : AbpModule
{

}
