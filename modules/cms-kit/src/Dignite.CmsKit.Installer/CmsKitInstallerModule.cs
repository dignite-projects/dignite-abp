using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.CmsKit;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class CmsKitInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsKitInstallerModule>();
        });
    }
}
