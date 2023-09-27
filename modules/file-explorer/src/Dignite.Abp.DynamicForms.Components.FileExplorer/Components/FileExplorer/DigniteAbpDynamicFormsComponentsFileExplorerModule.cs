using Dignite.Abp.DynamicForms.FileExplorer;
using Dignite.FileExplorer.Blazor;
using Volo.Abp.Modularity;

namespace Dignite.Abp.DynamicForms.Components.BlazoriseUI.FileExplorer;

[DependsOn(
    typeof(DigniteAbpDynamicFormsComponentsModule),
    typeof(DigniteAbpDynamicFormsFileExplorerModule),
    typeof(FileExplorerBlazorModule)
    )]
public class DigniteAbpDynamicFormsComponentsFileExplorerModule : AbpModule
{
}
