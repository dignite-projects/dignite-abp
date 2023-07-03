using Microsoft.AspNetCore.SignalR;
using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Dignite.FileExplorer.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(FileExplorerBlazorModule)
    )]
public class FileExplorerBlazorServerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // Error uploading file in Blazor server mode: "Did not receive any data in the allocated time"
        // Please refer to the following link for details
        // https://github.com/dotnet/aspnetcore/issues/38842#issuecomment-1342540950
        Configure<HubOptions>(options =>
        {
            options.DisableImplicitFromServicesParameters = true;
        });
    }
}
