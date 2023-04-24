using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Localization.MultiTenancy;

[DependsOn(
    typeof(AbpLocalizationModule)
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