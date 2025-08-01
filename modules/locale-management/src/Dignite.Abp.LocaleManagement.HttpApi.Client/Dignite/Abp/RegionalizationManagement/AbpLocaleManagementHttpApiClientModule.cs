using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.LocaleManagement;

[DependsOn(
    typeof(AbpLocaleManagementApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class AbpLocaleManagementHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(AbpLocaleManagementApplicationContractsModule).Assembly,
            LocaleManagementRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpLocaleManagementHttpApiClientModule>();
        });

    }
}
