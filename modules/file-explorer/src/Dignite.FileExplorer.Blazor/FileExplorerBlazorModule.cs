using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Dignite.FileExplorer.Blazor;

[DependsOn(
    typeof(FileExplorerApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule)
    )]
public class FileExplorerBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<FileExplorerBlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<FileExplorerBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(FileExplorerBlazorModule).Assembly);
        });
    }
}
