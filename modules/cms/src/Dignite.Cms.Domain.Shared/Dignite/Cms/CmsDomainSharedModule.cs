using Dignite.Abp.DynamicForms;
using Dignite.Abp.DynamicForms.Localization;
using Dignite.Cms.Localization;
using Volo.CmsKit;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Dignite.FileExplorer;

namespace Dignite.Cms;
[DependsOn(
    typeof(AbpDynamicFormsModule),
    typeof(CmsKitDomainSharedModule),
    typeof(FileExplorerDomainSharedModule)
)]
public class CmsDomainSharedModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        CmsGlobalFeatureConfigurator.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<CmsResource>("en")
                .AddBaseTypes(typeof(AbpDynamicFormsResource))
                .AddVirtualJson("/Dignite/Cms/Localization/Resources");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("Cms", typeof(CmsResource));
        });
    }
}
