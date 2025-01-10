using Localization.Resources.AbpUi;
using Dignite.Abp.RegionalizationManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Abp.RegionalizationManagement;

[DependsOn(
    typeof(RegionalizationManagementApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class RegionalizationManagementHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(RegionalizationManagementHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<RegionalizationManagementResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
