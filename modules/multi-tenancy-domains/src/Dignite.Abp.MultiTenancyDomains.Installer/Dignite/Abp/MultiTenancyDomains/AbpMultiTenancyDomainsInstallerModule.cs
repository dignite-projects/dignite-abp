using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.MultiTenancyDomains;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class AbpMultiTenancyDomainsInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpMultiTenancyDomainsInstallerModule>();
        });
    }
}
