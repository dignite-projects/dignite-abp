using Dignite.FileExplorer;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.CmsKit.Admin;

namespace Dignite.CmsKit.Admin;

[DependsOn(
    typeof(DigniteCmsKitAdminApplicationContractsModule),
    typeof(CmsKitAdminHttpApiClientModule),
    typeof(FileExplorerHttpApiClientModule))]
public class DigniteCmsKitAdminHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(DigniteCmsKitAdminApplicationContractsModule).Assembly,
            CmsKitAdminRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DigniteCmsKitAdminHttpApiClientModule>();
        });

    }
}
