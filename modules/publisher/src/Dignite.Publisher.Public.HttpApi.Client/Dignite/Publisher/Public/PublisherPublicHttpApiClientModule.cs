
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.CmsKit.Public;

namespace Dignite.Publisher.Public;

[DependsOn(
    typeof(CmsKitPublicHttpApiClientModule),
    typeof(PublisherPublicApplicationContractsModule),
    typeof(PublisherCommonHttpApiClientModule))]
public class PublisherPublicHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(PublisherPublicApplicationContractsModule).Assembly,
            PublisherPublicRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<PublisherPublicHttpApiClientModule>();
        });

    }
}
