using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.Localization;

[DependsOn(
    typeof(AbpMultiTenancyModule)
    )]
public class DigniteAbpLocalizationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMultiTenancyLocalization(options =>
        {
            options.ResourcesPath = "tenants";
        });
    }
}