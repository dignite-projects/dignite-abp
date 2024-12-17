using Dignite.CmsKit.Admin;
using Dignite.CmsKit.Localization;
using Dignite.CmsKit.Public;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace Dignite.CmsKit;

[DependsOn(
    typeof(DigniteCmsKitApplicationContractsModule),
    typeof(DigniteCmsKitPublicHttpApiModule),
    typeof(DigniteCmsKitAdminHttpApiModule),
    typeof(CmsKitHttpApiModule))]
public class DigniteCmsKitHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DigniteCmsKitHttpApiModule).Assembly);
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
