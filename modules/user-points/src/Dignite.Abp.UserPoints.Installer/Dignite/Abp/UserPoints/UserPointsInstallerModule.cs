using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.UserPoints;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class UserPointsInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<UserPointsInstallerModule>();
        });
    }
}
