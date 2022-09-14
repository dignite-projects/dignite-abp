using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Dignite.FileExplorer;

[DependsOn(
    typeof(FileExplorerDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class FileExplorerApplicationContractsModule : AbpModule
{
}