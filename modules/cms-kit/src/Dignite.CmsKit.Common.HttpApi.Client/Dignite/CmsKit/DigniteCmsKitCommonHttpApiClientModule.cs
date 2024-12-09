using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.CmsKit;

namespace Dignite.CmsKit;

[DependsOn(
    typeof(DigniteCmsKitCommonApplicationContractsModule),
    typeof(CmsKitCommonHttpApiClientModule))]
public class DigniteCmsKitCommonHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(DigniteCmsKitCommonApplicationContractsModule).Assembly,
            CmsKitCommonRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DigniteCmsKitCommonHttpApiClientModule>();
        });

    }
}
