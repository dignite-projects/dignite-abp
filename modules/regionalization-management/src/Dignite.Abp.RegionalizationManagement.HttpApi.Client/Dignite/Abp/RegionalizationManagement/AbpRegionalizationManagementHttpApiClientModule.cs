using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.RegionalizationManagement;

[DependsOn(
    typeof(AbpRegionalizationManagementApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class AbpRegionalizationManagementHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(AbpRegionalizationManagementApplicationContractsModule).Assembly,
            RegionalizationManagementRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpRegionalizationManagementHttpApiClientModule>();
        });

    }
}
