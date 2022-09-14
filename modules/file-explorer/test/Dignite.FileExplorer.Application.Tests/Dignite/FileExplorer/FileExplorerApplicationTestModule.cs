using Volo.Abp.Modularity;

namespace Dignite.FileExplorer;

[DependsOn(
    typeof(FileExplorerApplicationModule),
    typeof(FileExplorerDomainTestModule)
    )]
public class FileExplorerApplicationTestModule : AbpModule
{
}