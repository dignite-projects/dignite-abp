using Dignite.Abp.Regionalization.Resources;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.Regionalization;

[DependsOn(
    typeof(AbpLocalizationModule)
    )]
public class AbpRegionalizationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpRegionalizationModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options
                .Resources
                .Add<AbpRegionalizationResource>("en")
                .AddVirtualJson("/Dignite/Abp/Regionalization/Resources");
        });
    }
}
