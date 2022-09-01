using Localization.Resources.AbpUi;
using Dignite.Abp.FileManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Dignite.Abp.FileManagement.Files;

namespace Dignite.Abp.FileManagement;

[DependsOn(
    typeof(FileManagementApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class FileManagementHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(FileManagementHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<FileManagementResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });


        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(CreateFileInput));
        });
    }
}
