using Localization.Resources.AbpUi;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Dignite.Abp.UserPoints.Localization;

namespace Dignite.Abp.UserPoints;

[DependsOn(
    typeof(UserPointsApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class UserPointsHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(UserPointsHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<UserPointsResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
