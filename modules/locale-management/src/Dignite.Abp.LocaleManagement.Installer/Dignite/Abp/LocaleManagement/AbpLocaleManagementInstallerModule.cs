using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.LocaleManagement;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class AbpLocaleManagementInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpLocaleManagementInstallerModule>();
        });
    }
}
