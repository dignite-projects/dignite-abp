using Localization.Resources.AbpUi;
using Dignite.Abp.TenantDomains.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Abp.TenantDomains;

[DependsOn(
    typeof(AbpTenantDomainsApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class AbpTenantDomainsHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpTenantDomainsHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<TenantDomainsResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
