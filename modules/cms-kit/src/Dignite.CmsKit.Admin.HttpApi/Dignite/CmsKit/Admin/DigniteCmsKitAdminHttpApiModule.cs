using Dignite.CmsKit.Localization;
using Dignite.FileExplorer;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.CmsKit.Admin;

namespace Dignite.CmsKit.Admin;

[DependsOn(
    typeof(DigniteCmsKitAdminApplicationContractsModule),
    typeof(CmsKitAdminHttpApiModule),
    typeof(DigniteCmsKitCommonHttpApiModule),
    typeof(FileExplorerHttpApiModule))]
public class DigniteCmsKitAdminHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DigniteCmsKitAdminHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<DigniteCmsKitResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
