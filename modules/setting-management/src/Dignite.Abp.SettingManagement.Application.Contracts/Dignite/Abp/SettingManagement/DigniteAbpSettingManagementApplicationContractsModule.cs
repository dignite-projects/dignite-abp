using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Volo.Abp.Authorization;
using Dignite.Abp.FieldCustomizing;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Localization;
using Dignite.Abp.SettingManagement.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Dignite.Abp.Settings;

namespace Dignite.Abp.SettingManagement
{
    [DependsOn(
        typeof(AbpSettingManagementDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationAbstractionsModule),
        typeof(DigniteAbpFieldCustomizingModule),
        typeof(DigniteAbpSettingsModule)
    )]
    public class DigniteAbpSettingManagementApplicationContractsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DigniteAbpSettingManagementApplicationContractsModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<DigniteAbpSettingManagementResource>("en")
                    .AddBaseTypes(typeof(Volo.Abp.SettingManagement.Localization.AbpSettingManagementResource))
                    .AddVirtualJson("/Dignite/Abp/SettingManagement/Localization");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Dignite.Abp.SettingManagement", typeof(DigniteAbpSettingManagementResource));
            });
        }
    }
}
