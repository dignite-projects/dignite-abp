using Localization.Resources.AbpUi;
using Dignite.Abp.LocaleManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Abp.LocaleManagement;

[DependsOn(
    typeof(AbpLocaleManagementApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class AbpLocaleManagementHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpLocaleManagementHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<LocaleManagementResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
