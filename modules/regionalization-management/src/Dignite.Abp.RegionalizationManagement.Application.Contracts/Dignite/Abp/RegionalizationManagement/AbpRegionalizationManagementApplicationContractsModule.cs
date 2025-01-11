using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Dignite.Abp.Regionalization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Localization;
using Volo.Abp.VirtualFileSystem;
using Dignite.Abp.RegionalizationManagement.Localization;
using Volo.Abp.Validation.Localization;
using Volo.Abp.Validation;

namespace Dignite.Abp.RegionalizationManagement;

[DependsOn(
    typeof(AbpRegionalizationModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
    typeof(AbpValidationModule)
    )]
public class AbpRegionalizationManagementApplicationContractsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpRegionalizationManagementApplicationContractsModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<RegionalizationManagementResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Dignite/Abp/RegionalizationManagement/Localization");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("Dignite.Abp.RegionalizationManagement", typeof(RegionalizationManagementResource));
        });
    }

}
