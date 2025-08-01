using Dignite.Abp.Locales.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.Locales;

[DependsOn(
    typeof(AbpLocalizationModule)
    )]
public class AbpLocalesModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpLocalesModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options
                .Resources
                .Add<AbpLocaleResource>("en")
                .AddVirtualJson("/Dignite/Abp/Locales/Localization");
        });
    }
}
