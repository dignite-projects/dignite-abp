using Dignite.FileExplorer;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.CmsKit.Public;

namespace Dignite.CmsKit.Public;

[DependsOn(
    typeof(DigniteCmsKitPublicApplicationContractsModule),
    typeof(CmsKitPublicHttpApiClientModule),
    typeof(DigniteCmsKitCommonHttpApiClientModule)
    typeof(FileExplorerHttpApiClientModule))]
public class DigniteCmsKitPublicHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(DigniteCmsKitPublicApplicationContractsModule).Assembly,
            CmsKitPublicRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DigniteCmsKitPublicHttpApiClientModule>();
        });

    }
}
