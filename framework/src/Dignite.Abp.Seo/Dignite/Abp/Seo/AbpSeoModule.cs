using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.Modularity;
using Volo.Abp.Ui.Branding;

namespace Dignite.Abp.Seo;

[DependsOn(
    typeof(AbpAspNetCoreMvcUiModule)
    )]
public class AbpSeoModule: AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSeoTags((serviceProvider, seoInfo)=>
        {
            var brandingProvider = serviceProvider.GetRequiredService<IBrandingProvider>();
            seoInfo.SetSiteInfo(brandingProvider?.AppName);
            seoInfo.SetLocales(CultureInfo.CurrentUICulture.Name);
        });
    }
}
