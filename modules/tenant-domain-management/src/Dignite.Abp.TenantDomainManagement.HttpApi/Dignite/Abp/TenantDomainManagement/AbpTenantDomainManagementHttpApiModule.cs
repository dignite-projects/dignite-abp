using Localization.Resources.AbpUi;
using Dignite.Abp.TenantDomainManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Abp.TenantDomainManagement;

[DependsOn(
    typeof(AbpTenantDomainManagementApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class AbpTenantDomainManagementHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpTenantDomainManagementHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<TenantDomainManagementResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
