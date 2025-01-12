using Localization.Resources.AbpUi;
using Dignite.Abp.MultiTenancyDomains.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Abp.MultiTenancyDomains;

[DependsOn(
    typeof(AbpMultiTenancyDomainsApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class AbpMultiTenancyDomainsHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpMultiTenancyDomainsHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<MultiTenancyDomainsResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
