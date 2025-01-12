using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.MultiTenancyLocalization;

[DependsOn(
    typeof(AbpMultiTenancyModule)
    )]
public class AbpMultiTenancyLocalizationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMultiTenancyLocalization(options =>
        {
            options.ResourcesPath = "tenants";
        });
    }
}