using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.Localization.MultiTenancy;

[DependsOn(
    typeof(AbpLocalizationModule),
    typeof(AbpMultiTenancyModule)
    )]
public class AbpLocalizationMultiTenancyModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMultiTenancyLocalization(options =>
        {
            options.ResourcesPath = "tenants";
        });
    }
}