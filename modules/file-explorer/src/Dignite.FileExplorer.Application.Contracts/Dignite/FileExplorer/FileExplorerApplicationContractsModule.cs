using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Dignite.FileExplorer;

[DependsOn(
    typeof(FileExplorerDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class FileExplorerApplicationContractsModule : AbpModule
{

}
