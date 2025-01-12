using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.MultiTenancyDomains;

[DependsOn(
    typeof(AbpMultiTenancyDomainsApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class AbpMultiTenancyDomainsHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(AbpMultiTenancyDomainsApplicationContractsModule).Assembly,
            MultiTenancyDomainsRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpMultiTenancyDomainsHttpApiClientModule>();
        });

    }
}
