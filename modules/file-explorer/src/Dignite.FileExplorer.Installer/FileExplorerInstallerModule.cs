using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.FileExplorer;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class FileExplorerInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<FileExplorerInstallerModule>();
        });
    }
}