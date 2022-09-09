using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Identity;

namespace Dignite.Abp.NotificationCenter;

[DependsOn(
    typeof(AbpIdentityApplicationContractsModule),
    typeof(AbpNotificationCenterDomainModule),
    typeof(AbpNotificationCenterApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class AbpNotificationCenterApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpNotificationCenterApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AbpNotificationCenterApplicationModule>(validate: true);
        });
    }
}
