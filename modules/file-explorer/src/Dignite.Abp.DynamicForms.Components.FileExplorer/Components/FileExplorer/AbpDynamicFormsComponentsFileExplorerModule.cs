using Dignite.Abp.DynamicForms.FileExplorer;
using Dignite.FileExplorer.Blazor;
using Volo.Abp.Modularity;

namespace Dignite.Abp.DynamicForms.Components.BlazoriseUI.FileExplorer;

[DependsOn(
    typeof(AbpDynamicFormsComponentsModule),
    typeof(AbpDynamicFormsFileExplorerModule),
    typeof(FileExplorerBlazorModule)
    )]
public class AbpDynamicFormsComponentsFileExplorerModule : AbpModule
{
}
