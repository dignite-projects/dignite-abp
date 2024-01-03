using Dignite.FileExplorer.Directories;
using Dignite.FileExplorer.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Imaging;
using Volo.Abp.Modularity;

namespace Dignite.FileExplorer;

[DependsOn(
    typeof(FileExplorerDomainModule),
    typeof(FileExplorerApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpImagingAbstractionsModule),
    typeof(AbpImagingImageSharpModule)
    )]
public class FileExplorerApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<FileExplorerApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<FileExplorerApplicationModule>(validate: true);
        });

        Configure<AuthorizationOptions>(options =>
        {
            options.AddPolicy("DigniteFileExplorerManagePolicy", policy => policy.Requirements.Add(CommonOperations.Create));
            options.AddPolicy("DigniteFileExplorerUpdatePolicy", policy => policy.Requirements.Add(CommonOperations.Update));
            options.AddPolicy("DigniteFileExplorerDeletePolicy", policy => policy.Requirements.Add(CommonOperations.Delete));
            options.AddPolicy("DigniteFileExplorerGetPolicy", policy => policy.Requirements.Add(CommonOperations.Get));
        });

        context.Services.AddSingleton<IAuthorizationHandler, FileDescriptorAuthorizationHandler>();
        context.Services.AddSingleton<IAuthorizationHandler, DirectoryDescriptorAuthorizationHandler>();
    }
}