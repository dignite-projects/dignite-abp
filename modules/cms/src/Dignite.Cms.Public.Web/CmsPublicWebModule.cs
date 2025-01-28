using Dignite.Abp.AspNetCore.Mvc.Regionalization;
using Dignite.Abp.MultiTenancyLocalization;
using Dignite.Cms.Localization;
using Dignite.Cms.Public.Web.Toolbars;
using Volo.CmsKit.Public.Web;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Cms.Public.Web;

[DependsOn(
    typeof(CmsPublicApplicationContractsModule),
    typeof(CmsKitPublicWebModule),
    typeof(AbpMultiTenancyLocalizationModule),
    typeof(AbpAspNetCoreMvcRegionalizationModule)
    )]
public class CmsPublicWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(CmsResource), typeof(CmsPublicWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CmsPublicWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsPublicWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<CmsPublicWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<CmsPublicWebModule>(validate: true);
        });


        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new CmsToolbarContributor());
        });
    }
}
