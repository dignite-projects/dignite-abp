using Dignite.Abp.BlazoriseUI;
using Dignite.Abp.DynamicForms.CkEditor;
using Volo.Abp.Modularity;

namespace Dignite.Abp.DynamicForms.Components.BlazoriseUI.CkEditor;

[DependsOn(
    typeof(DigniteAbpBlazoriseUiModule),
    typeof(DigniteAbpDynamicFormsComponentsModule),
    typeof(DigniteAbpDynamicFormsCkEditorModule)
    )]
public class DigniteAbpDynamicFormsComponentsCkEditorModule : AbpModule
{
}
