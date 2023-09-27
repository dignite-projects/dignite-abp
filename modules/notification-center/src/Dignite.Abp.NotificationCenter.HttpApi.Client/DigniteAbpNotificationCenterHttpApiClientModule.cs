using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.NotificationCenter;

[DependsOn(
    typeof(DigniteAbpNotificationCenterApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class DigniteAbpNotificationCenterHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(DigniteAbpNotificationCenterApplicationContractsModule).Assembly,
            NotificationCenterRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DigniteAbpNotificationCenterHttpApiClientModule>();
        });
    }
}