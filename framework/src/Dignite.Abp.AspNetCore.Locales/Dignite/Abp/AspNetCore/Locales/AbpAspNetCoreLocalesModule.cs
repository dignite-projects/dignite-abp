using Dignite.Abp.AspNetCore.Locales.Routing;
using Dignite.Abp.Locales;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AspNetCore.Locales;

[DependsOn(typeof(AbpAspNetCoreMvcModule))]
[DependsOn(typeof(AbpLocalesModule))]
public class AbpAspNetCoreLocalesModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddRouting(options =>
        {
            options.ConstraintMap.Add(LocaleRouteConstraint.ConstraintName, typeof(LocaleRouteConstraint));
        });
    }
}
