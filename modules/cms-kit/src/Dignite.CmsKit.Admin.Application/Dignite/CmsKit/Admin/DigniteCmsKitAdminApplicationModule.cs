using Dignite.CmsKit.BlobStoring;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.BlobStoring;
using Volo.Abp.Modularity;
using Volo.CmsKit.Admin;
using Dignite.Abp.BlobStoring;
using Dignite.CmsKit.Permissions;
using Dignite.FileExplorer;

namespace Dignite.CmsKit.Admin;

[DependsOn(
    typeof(DigniteCmsKitDomainModule),
    typeof(DigniteCmsKitAdminApplicationContractsModule),
    typeof(CmsKitAdminApplicationModule),
    typeof(DigniteCmsKitCommonApplicationModule),
    typeof(FileExplorerApplicationModule)
    )]
public class DigniteCmsKitAdminApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<DigniteCmsKitAdminApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<DigniteCmsKitAdminApplicationModule>(validate: true);
        });


        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers
                .Configure<BrandLogoContainer>(container =>
                {
                    container.SetBlobNameGenerator<BrandLogoBlobNameGenerator>();
                    container.AddFileSizeLimitHandler(handler =>
                    {
                        handler.MaxFileSize = 1024;
                    });
                    container.AddFileTypeCheckHandler(handler =>
                    {
                        handler.AllowedFileTypeNames = [".jpg", ".jpeg", ".png", ".webp"];
                    });
                    container.AddImageResizeHandler(handler =>
                    {
                        handler.ImageWidth = 300;
                        handler.ImageHeight = 300;
                    });
                    container.SetAuthorizationConfiguration(config =>
                    {
                        config.CreateFilePermissionName = CmsKitAdminPermissions.Brand.Update;
                        config.UpdateFilePermissionName = CmsKitAdminPermissions.Brand.Update;
                        config.DeleteFilePermissionName = CmsKitAdminPermissions.Brand.Update;
                    });
                })
                .Configure<BrandLogoReverseContainer>(container =>
                {
                    container.SetBlobNameGenerator<BrandLogoReverseBlobNameGenerator>();
                    container.AddFileSizeLimitHandler(handler =>
                    {
                        handler.MaxFileSize = 1024;
                    });
                    container.AddFileTypeCheckHandler(handler =>
                    {
                        handler.AllowedFileTypeNames = [".jpg", ".jpeg", ".png", ".webp"];
                    });
                    container.AddImageResizeHandler(handler =>
                    {
                        handler.ImageWidth = 300;
                        handler.ImageHeight = 300;
                    });
                    container.SetAuthorizationConfiguration(config =>
                    {
                        config.CreateFilePermissionName = CmsKitAdminPermissions.Brand.Update;
                        config.UpdateFilePermissionName = CmsKitAdminPermissions.Brand.Update;
                        config.DeleteFilePermissionName = CmsKitAdminPermissions.Brand.Update;
                    });
                })
                .Configure<BrandIconContainer>(container =>
                {
                    container.SetBlobNameGenerator<BrandIconBlobNameGenerator>();
                    container.AddFileSizeLimitHandler(handler =>
                    {
                        handler.MaxFileSize = 1024;
                    });
                    container.AddFileTypeCheckHandler(handler =>
                    {
                        handler.AllowedFileTypeNames = [".jpg", ".jpeg", ".png", ".webp", ".ico"];
                    });
                    container.AddImageResizeHandler(handler =>
                    {
                        handler.ImageWidth = 300;
                        handler.ImageHeight = 300;
                    });
                    container.SetAuthorizationConfiguration(config =>
                    {
                        config.CreateFilePermissionName = CmsKitAdminPermissions.Brand.Update;
                        config.UpdateFilePermissionName = CmsKitAdminPermissions.Brand.Update;
                        config.DeleteFilePermissionName = CmsKitAdminPermissions.Brand.Update;
                    });
                });
        });
    }
}
