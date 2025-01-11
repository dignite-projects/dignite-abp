using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.TenantDomains;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class AbpTenantDomainsInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpTenantDomainsInstallerModule>();
        });
    }
}
