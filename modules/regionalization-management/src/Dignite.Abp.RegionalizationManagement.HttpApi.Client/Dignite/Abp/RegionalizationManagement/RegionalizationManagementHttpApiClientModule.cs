using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.RegionalizationManagement;

[DependsOn(
    typeof(RegionalizationManagementApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class RegionalizationManagementHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(RegionalizationManagementApplicationContractsModule).Assembly,
            RegionalizationManagementRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<RegionalizationManagementHttpApiClientModule>();
        });

    }
}
