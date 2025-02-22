using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.TenantDomainManagement;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class AbpTenantDomainManagementInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpTenantDomainManagementInstallerModule>();
        });
    }
}
