using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.UserPoints;

[DependsOn(
    typeof(UserPointsApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class UserPointsHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(UserPointsApplicationContractsModule).Assembly,
            UserPointsRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<UserPointsHttpApiClientModule>();
        });

    }
}
