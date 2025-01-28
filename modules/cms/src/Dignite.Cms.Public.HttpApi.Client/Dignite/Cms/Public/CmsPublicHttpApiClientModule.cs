using Volo.CmsKit.Public;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Dignite.FileExplorer;

namespace Dignite.Cms.Public;

[DependsOn(
    typeof(CmsPublicApplicationContractsModule),
    typeof(CmsKitPublicHttpApiClientModule),
    typeof(FileExplorerHttpApiClientModule))]
public class CmsPublicHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(CmsPublicApplicationContractsModule).Assembly,
            CmsPublicRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsPublicHttpApiClientModule>();
        });
    }
}
