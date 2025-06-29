﻿using Dignite.Abp.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.AspNetCore.Mvc.UI.TenantTheme;

[DependsOn(typeof(Volo.Abp.AspNetCore.Mvc.UI.AbpAspNetCoreMvcUiModule))]
public class AbpAspNetCoreMvcUiTenantThemeModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<RazorViewEngineOptions>(options =>
        {
            var currentTenantLazy = context.Services.GetServiceLazy<ICurrentTenant>();
            var themeSelectorLazy = context.Services.GetServiceLazy<IThemeSelector>();
            options.ViewLocationExpanders.Add(new TenantViewLocationExpander(currentTenantLazy, themeSelectorLazy));
        });
    }
}
