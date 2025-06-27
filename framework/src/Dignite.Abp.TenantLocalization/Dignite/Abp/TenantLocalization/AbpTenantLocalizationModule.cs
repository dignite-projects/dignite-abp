using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.TenantLocalization;

[DependsOn(
    typeof(AbpMultiTenancyModule)
    )]
public class AbpTenantLocalizationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTenantLocalization(options =>
        {
            options.ResourcesPath = "Tenants";
        });
    }
}