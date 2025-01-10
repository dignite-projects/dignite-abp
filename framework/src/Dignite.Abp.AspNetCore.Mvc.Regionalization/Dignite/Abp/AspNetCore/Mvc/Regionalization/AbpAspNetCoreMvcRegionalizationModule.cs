using Dignite.Abp.AspNetCore.Mvc.Regionalization.Routing;
using Dignite.Abp.Regionalization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AspNetCore.Mvc.Regionalization;

[DependsOn(typeof(AbpAspNetCoreMvcModule))]
[DependsOn(typeof(AbpRegionalizationModule))]
public class AbpAspNetCoreMvcRegionalizationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddRouting(options =>
        {
            options.ConstraintMap.Add(RegionalizationRouteConstraint.ConstraintName, typeof(RegionalizationRouteConstraint));
        });
    }
}
