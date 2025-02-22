using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.TenantDomainManagement;

[DependsOn(
    typeof(AbpTenantDomainManagementApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class AbpTenantDomainManagementHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(AbpTenantDomainManagementApplicationContractsModule).Assembly,
            TenantDomainManagementRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpTenantDomainManagementHttpApiClientModule>();
        });

    }
}
