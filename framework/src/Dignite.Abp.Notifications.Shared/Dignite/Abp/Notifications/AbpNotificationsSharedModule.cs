using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Notifications;

[DependsOn(
    typeof(AbpLocalizationModule)
    )]
public class AbpNotificationsSharedModule : AbpModule
{
}