using Dignite.Abp.BlobStoring;
using Dignite.Abp.RegionalizationManagement;
using Dignite.Cms.Admin.Entries;
using Dignite.Cms.Permissions;
using Volo.CmsKit.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.BlobStoring;
using Volo.Abp.Modularity;
using Dignite.FileExplorer;

namespace Dignite.Cms.Admin;

[DependsOn(
    typeof(CmsDomainModule),
    typeof(CmsAdminApplicationContractsModule),
    typeof(AbpRegionalizationManagementApplicationModule),
    typeof(CmsKitAdminApplicationModule),
    typeof(FileExplorerApplicationModule)
    )]
public class CmsAdminApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<CmsAdminApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<CmsAdminApplicationModule>(validate: true);
        });

        Configure<AuthorizationOptions>(options =>
        {
            options.AddPolicy("DigniteCmsCreatePolicy", policy => policy.Requirements.Add(CommonOperations.Create));
            options.AddPolicy("DigniteCmsUpdatePolicy", policy => policy.Requirements.Add(CommonOperations.Update));
            options.AddPolicy("DigniteCmsDeletePolicy", policy => policy.Requirements.Add(CommonOperations.Delete));
        });



        //

        Configure<AbpBlobStoringOptions>(options =>
        {
            _ = options.Containers
                .Configure<CommonFileBlobContainer>(container =>
                {
                    container.AddFileSizeLimitHandler(handler =>
                    {
                        handler.MaxFileSize = 10240;
                    });
                    container.AddFileTypeCheckHandler(handler =>
                    {
                        handler.AllowedFileTypeNames = new string[] { ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".zip", ".7z", ".rar", ".mp4", ".mp3", ".pdf", ".gif", ".jpg", ".jpeg", ".png", ".webp" };
                    });
                    container.AddImageResizeHandler(handler =>
                    {
                        handler.ImageWidth = 1920;
                        handler.ImageHeight = 1080;
                    });
                    container.SetAuthorizationConfiguration(config =>
                    {
                        config.CreateDirectoryPermissionName = CmsAdminPermissions.Entry.Create;
                        config.CreateFilePermissionName = CmsAdminPermissions.Entry.Create;
                        config.UpdateFilePermissionName = CmsAdminPermissions.Entry.Update;
                        config.DeleteFilePermissionName = CmsAdminPermissions.Entry.Delete;
                        config.SetAuthorizationHandler<EntryResourceAuthorizationHandler>();
                    });
                });
            _ = options.Containers
                .Configure<ImageBlobContainer>(container =>
                {
                    container.AddFileSizeLimitHandler(handler =>
                    {
                        handler.MaxFileSize = 10240;
                    });
                    container.AddFileTypeCheckHandler(handler =>
                    {
                        handler.AllowedFileTypeNames = new string[] { ".gif", ".jpg", ".jpeg", ".png", ".webp" };
                    });
                    container.AddImageResizeHandler(handler =>
                    {
                        handler.ImageWidth = 1920;
                        handler.ImageHeight = 1080;
                    });
                    container.SetAuthorizationConfiguration(config =>
                    {
                        config.CreateDirectoryPermissionName = CmsAdminPermissions.Entry.Create;
                        config.CreateFilePermissionName = CmsAdminPermissions.Entry.Create;
                        config.UpdateFilePermissionName = CmsAdminPermissions.Entry.Update;
                        config.DeleteFilePermissionName = CmsAdminPermissions.Entry.Delete;
                        config.SetAuthorizationHandler<EntryResourceAuthorizationHandler>();
                    });
                });
        });
    }
}
