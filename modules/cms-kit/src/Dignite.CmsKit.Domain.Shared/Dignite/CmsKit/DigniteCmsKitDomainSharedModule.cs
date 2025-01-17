﻿
using Dignite.CmsKit.Localization;
using Dignite.FileExplorer;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Volo.CmsKit;

namespace Dignite.CmsKit;

[DependsOn(
    typeof(CmsKitDomainSharedModule),
    typeof(FileExplorerDomainSharedModule)
)]
public class DigniteCmsKitDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DigniteCmsKitDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<DigniteCmsKitResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Dignite/CmsKit/Localization/Resources");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("CmsKit", typeof(DigniteCmsKitResource));
        });
    }
}
