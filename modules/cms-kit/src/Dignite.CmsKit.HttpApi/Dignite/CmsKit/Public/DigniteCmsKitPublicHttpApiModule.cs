using Dignite.CmsKit.Localization;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Dignite.CmsKit.Public;

[DependsOn(
    typeof(AbpAspNetCoreMvcModule),
    typeof(DigniteCmsKitPublicApplicationContractsModule))]
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
