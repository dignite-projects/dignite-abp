using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Dignite.FileExplorer.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(FileExplorerBlazorModule)
    )]
public class FileExplorerBlazorServerModule : AbpModule
{

}
