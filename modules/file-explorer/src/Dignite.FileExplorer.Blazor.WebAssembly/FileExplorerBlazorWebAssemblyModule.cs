using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Dignite.FileExplorer.Blazor.WebAssembly;

[DependsOn(
    typeof(FileExplorerBlazorModule),
    typeof(FileExplorerHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class FileExplorerBlazorWebAssemblyModule : AbpModule
{

}
