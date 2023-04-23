using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Dignite.Abp.TenantLocalization;

[DependsOn(
    typeof(AbpLocalizationModule)
    )]
public class AbpTenantLocalizationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTenantLocalization(options =>
        {
            options.ResourcesPath = "tenants";
        });
    }
}