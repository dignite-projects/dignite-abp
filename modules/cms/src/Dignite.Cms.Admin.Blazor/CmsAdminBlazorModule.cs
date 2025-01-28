using Dignite.Abp.BlazoriseUI;
using Dignite.Abp.DynamicForms.Components.BlazoriseUI;
using Dignite.Abp.DynamicForms.Components.BlazoriseUI.CkEditor;
using Dignite.Abp.DynamicForms.Components.BlazoriseUI.FileExplorer;
using Dignite.Cms.Admin.Blazor.Menus;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Dignite.Cms.Admin.Blazor;

[DependsOn(
    typeof(CmsAdminApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule),
    typeof(DigniteAbpBlazoriseUiModule),
    typeof(AbpDynamicFormsComponentsBlazoriseUiModule),
    typeof(AbpDynamicFormsComponentsFileExplorerModule),
    typeof(AbpDynamicFormsComponentsCkEditorModule)
    )]
public class CmsAdminBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<CmsAdminBlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<CmsAdminBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new CmsAdminMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(CmsAdminBlazorModule).Assembly);
        });
    }
}