
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.CmsKit.Admin;

namespace Dignite.Publisher.Admin;

[DependsOn(
    typeof(CmsKitAdminHttpApiClientModule),
    typeof(PublisherAdminApplicationContractsModule),
    typeof(PublisherCommonHttpApiClientModule))]
public class PublisherAdminHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(PublisherAdminApplicationContractsModule).Assembly,
            PublisherAdminRemoteServiceConsts.RemoteServiceName
            );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<PublisherAdminHttpApiClientModule>();
        });
    }
}
