using Dignite.Abp.DynamicForms.FileExplorer.Localization;
using Dignite.Abp.DynamicForms.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.DynamicForms.FileExplorer;

[DependsOn(
    typeof(DigniteAbpDynamicFormsModule)
    )]
public class DigniteAbpDynamicFormsFileExplorerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DigniteAbpDynamicFormsFileExplorerModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<AbpDynamicFormsFileExplorerResource>("en")
                .AddVirtualJson("/Dignite/Abp/DynamicForms/FileExplorer/Localization/Resources")
                .AddBaseTypes(typeof(AbpDynamicFormsResource)); //Inherit from an existing resource
        });
    }
}