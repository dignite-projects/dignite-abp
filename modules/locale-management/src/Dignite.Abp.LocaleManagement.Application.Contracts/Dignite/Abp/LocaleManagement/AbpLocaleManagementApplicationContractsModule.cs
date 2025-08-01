using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Dignite.Abp.Locales;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Localization;
using Volo.Abp.VirtualFileSystem;
using Dignite.Abp.LocaleManagement.Localization;
using Volo.Abp.Validation.Localization;
using Volo.Abp.Validation;

namespace Dignite.Abp.LocaleManagement;

[DependsOn(
    typeof(AbpLocalesModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
    typeof(AbpValidationModule)
    )]
public class AbpLocaleManagementApplicationContractsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpLocaleManagementApplicationContractsModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<LocaleManagementResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Dignite/Abp/LocaleManagement/Localization");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("Dignite.Abp.LocaleManagement", typeof(LocaleManagementResource));
        });
    }

}
