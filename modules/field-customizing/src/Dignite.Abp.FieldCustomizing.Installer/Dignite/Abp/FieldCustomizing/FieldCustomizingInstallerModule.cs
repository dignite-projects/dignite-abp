using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.FieldCustomizing;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class FieldCustomizingInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<FieldCustomizingInstallerModule>();
        });
    }
}