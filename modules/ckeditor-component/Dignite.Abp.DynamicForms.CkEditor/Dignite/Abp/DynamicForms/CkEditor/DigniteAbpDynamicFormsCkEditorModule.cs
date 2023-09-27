using Dignite.Abp.DynamicForms.CkEditor.Localization;
using Dignite.Abp.DynamicForms.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.DynamicForms.CkEditor;

[DependsOn(
    typeof(DigniteAbpDynamicFormsModule)
    )]
public class DigniteAbpDynamicFormsCkEditorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DigniteAbpDynamicFormsCkEditorModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<AbpDynamicFormsCkEditorResource>("en")
                .AddVirtualJson("/Dignite/Abp/DynamicForms/CkEditor/Localization/Resources")
                .AddBaseTypes(typeof(AbpDynamicFormsResource)); //Inherit from an existing resource
        });
    }
}