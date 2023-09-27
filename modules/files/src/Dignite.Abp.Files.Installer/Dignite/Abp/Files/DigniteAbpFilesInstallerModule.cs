using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.Files;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class DigniteAbpFilesInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DigniteAbpFilesInstallerModule>();
        });
    }
}