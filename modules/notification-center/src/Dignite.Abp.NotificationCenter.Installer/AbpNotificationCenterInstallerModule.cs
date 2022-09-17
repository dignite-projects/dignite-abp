using Volo.Abp.Modularity;
using Volo.Abp.Studio;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.NotificationCenter;

[DependsOn(
    typeof(AbpStudioModuleInstallerModule),
    typeof(AbpVirtualFileSystemModule)
    )]
public class AbpNotificationCenterInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpNotificationCenterInstallerModule>();
        });
    }
}