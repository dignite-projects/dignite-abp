using Dignite.Abp.SettingManagement.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.SettingManagement;

[DependsOn(
    typeof(Volo.Abp.SettingManagement.AbpSettingManagementDomainSharedModule)
    )]
public class AbpSettingManagementDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpSettingManagementDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<AbpSettingManagementResource>("en")
                .AddBaseTypes(
                    typeof(Volo.Abp.SettingManagement.Localization.AbpSettingManagementResource)
                ).AddVirtualJson("/Dignite/Abp/SettingManagement/Localization/Resources");
        });
    }
}