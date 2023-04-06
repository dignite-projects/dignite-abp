using Dignite.Abp.BlazoriseUI;
using Dignite.Abp.DynamicForms.CkEditor;
using Volo.Abp.Modularity;

namespace Dignite.Abp.DynamicForms.Components.BlazoriseUI.CkEditor;

[DependsOn(
    typeof(AbpBlazoriseUiModule),
    typeof(AbpDynamicFormsComponentsModule),
    typeof(AbpDynamicFormsCkEditorModule)
    )]
public class AbpDynamicFormsComponentsCkEditorModule : AbpModule
{
}
