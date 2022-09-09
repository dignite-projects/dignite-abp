using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Dignite.Abp.NotificationCenter.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Dignite.Abp.Notifications;

namespace Dignite.Abp.NotificationCenter;

[DependsOn(
    typeof(AbpValidationModule),
    typeof(AbpNotificationsSharedModule)
)]
public class AbpNotificationCenterDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpNotificationCenterDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<NotificationCenterResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Dignite/Abp/NotificationCenter/Localization");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("NotificationCenter", typeof(NotificationCenterResource));
        });
    }
}
