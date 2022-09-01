using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.NotificationCenter
{
    [DependsOn(
        typeof(NotificationCenterApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class NotificationCenterHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(NotificationCenterApplicationContractsModule).Assembly,
                NotificationCenterRemoteServiceConsts.RemoteServiceName
            );

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NotificationCenterHttpApiClientModule>();
            });

        }
    }
}
