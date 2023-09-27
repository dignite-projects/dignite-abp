using Dignite.Abp.DynamicForms.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.DynamicForms;

[DependsOn(
    typeof(AbpLocalizationModule)
    )]
public class DigniteAbpDynamicFormsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DigniteAbpDynamicFormsModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<AbpDynamicFormsResource>("en")
                .AddVirtualJson("/Dignite/Abp/DynamicForms/Localization/Resources");
        });
    }
}