using Localization.Resources.AbpUi;
using Dignite.Abp.NotificationCenter.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Abp.NotificationCenter
{
    [DependsOn(
        typeof(NotificationCenterApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class NotificationCenterHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(NotificationCenterHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<NotificationCenterResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
