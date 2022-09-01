using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Identity;

namespace Dignite.Abp.NotificationCenter
{
    [DependsOn(
        typeof(AbpIdentityApplicationContractsModule),
        typeof(NotificationCenterDomainModule),
        typeof(NotificationCenterApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class NotificationCenterApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<NotificationCenterApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<NotificationCenterApplicationModule>(validate: true);
            });
        }
    }
}
