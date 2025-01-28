using Dignite.Abp.RegionalizationManagement;
using Dignite.Cms.Localization;
using Volo.CmsKit.Admin;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Dignite.FileExplorer;

namespace Dignite.Cms.Admin;

[DependsOn(
    typeof(CmsAdminApplicationContractsModule),
    typeof(AbpRegionalizationManagementHttpApiModule),
    typeof(CmsKitAdminHttpApiModule),
    typeof(FileExplorerHttpApiModule))]
public class CmsAdminHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CmsAdminHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<CmsResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
