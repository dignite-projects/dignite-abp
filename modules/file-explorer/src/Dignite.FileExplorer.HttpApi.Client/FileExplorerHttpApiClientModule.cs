using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.FileExplorer;

[DependsOn(
    typeof(FileExplorerApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class FileExplorerHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(FileExplorerApplicationContractsModule).Assembly,
            FileExplorerRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<FileExplorerHttpApiClientModule>();
        });
    }
}