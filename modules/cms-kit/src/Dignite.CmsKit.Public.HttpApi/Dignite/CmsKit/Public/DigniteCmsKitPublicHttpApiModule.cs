using Dignite.CmsKit.Localization;
using Dignite.FileExplorer;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.CmsKit.Public;

namespace Dignite.CmsKit.Public;

[DependsOn(
    typeof(DigniteCmsKitPublicApplicationContractsModule),
    typeof(CmsKitPublicHttpApiModule),
    typeof(DigniteCmsKitCommonHttpApiModule),
    typeof(FileExplorerHttpApiModule))]
public class DigniteCmsKitPublicHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DigniteCmsKitPublicHttpApiModule).Assembly);
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
