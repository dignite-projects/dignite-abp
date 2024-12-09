using Dignite.CmsKit.Localization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AutoMapper;
using Volo.Abp.Http.ProxyScripting.Generators.JQuery;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.CmsKit.Public.Web;

namespace Dignite.CmsKit.Public.Web;

[DependsOn(
    typeof(DigniteCmsKitPublicApplicationContractsModule),
    typeof(CmsKitPublicWebModule)
    )]
public class DigniteCmsKitPublicWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(DigniteCmsKitResource), typeof(DigniteCmsKitPublicWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DigniteCmsKitPublicWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DigniteCmsKitPublicWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<DigniteCmsKitPublicWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<DigniteCmsKitPublicWebModule>(validate: true);
        });

        Configure<DynamicJavaScriptProxyOptions>(options =>
        {
            options.DisableModule(CmsKitPublicRemoteServiceConsts.ModuleName);
        });
    }
}
