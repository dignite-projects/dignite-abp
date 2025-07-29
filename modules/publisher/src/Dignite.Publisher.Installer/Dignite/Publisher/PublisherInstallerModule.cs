using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Publisher;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class PublisherInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<PublisherInstallerModule>();
        });
    }
}
