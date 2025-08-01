using Dignite.Abp.LocaleManagement;
using Volo.CmsKit.Admin;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Dignite.FileExplorer;

namespace Dignite.Cms.Admin;

[DependsOn(
    typeof(CmsAdminApplicationContractsModule),
    typeof(CmsKitAdminHttpApiClientModule),
    typeof(AbpLocaleManagementHttpApiClientModule),
    typeof(FileExplorerHttpApiClientModule))]
public class CmsAdminHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(CmsAdminApplicationContractsModule).Assembly,
            CmsAdminRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsAdminHttpApiClientModule>();
        });

    }
}
