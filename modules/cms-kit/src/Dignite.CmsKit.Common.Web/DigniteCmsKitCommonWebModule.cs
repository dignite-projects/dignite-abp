using Dignite.CmsKit.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AutoMapper;
using Volo.Abp.Http.ProxyScripting.Generators.JQuery;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.CmsKit.Web;

namespace Dignite.CmsKit.Common.Web;

[DependsOn(
    typeof(DigniteCmsKitCommonApplicationContractsModule),
    typeof(CmsKitCommonWebModule)
    )]
public class DigniteCmsKitCommonWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(DigniteCmsKitResource), typeof(DigniteCmsKitCommonWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DigniteCmsKitCommonWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DigniteCmsKitCommonWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<DigniteCmsKitCommonWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<DigniteCmsKitCommonWebModule>(validate: true);
        });

        Configure<DynamicJavaScriptProxyOptions>(options =>
        {
            options.DisableModule(CmsKitCommonRemoteServiceConsts.ModuleName);
        });
    }
}
