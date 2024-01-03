using System;
using System.IO;
using Dignite.Abp.BlobStoring;
using Dignite.FileExplorer.TestObjects;
using Volo.Abp;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.IO;
using Volo.Abp.Modularity;

namespace Dignite.FileExplorer;

[DependsOn(
    typeof(FileExplorerApplicationModule),
    typeof(FileExplorerDomainTestModule),
    typeof(AbpBlobStoringFileSystemModule)
    )]
public class FileExplorerApplicationTestModule : AbpModule
{

    private readonly string _testDirectoryPath;

    public FileExplorerApplicationTestModule()
    {
        _testDirectoryPath = Path.Combine(
            Path.GetTempPath(),
            Guid.NewGuid().ToString("N")
        );
    }
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers
                .Configure<TestContainer>(container =>
                {
                    container.AddImageResizeHandler(imageResize =>
                    {
                        imageResize.ImageWidth = 200;
                        imageResize.ImageHeight = 200;
                        imageResize.ImageSizeMustBeLargerThanPreset = true;
                    });
                });
        });
        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers
                .Configure<TestContainer1>(container =>
                {
                    container.SetAuthorizationConfiguration(config =>
                    {
                        config.CreateDirectoryPermissionName = "permissionName1";
                        config.CreateFilePermissionName = "permissionName2";
                        config.UpdateFilePermissionName = "permissionName2";
                        config.DeleteFilePermissionName = "permissionName2";
                        config.SetAuthorizationHandler<TestFileDescriptorEntityAuthorizationHandler>();
                    });
                });
        });

        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureAll((containerName, containerConfiguration) =>
            {
                containerConfiguration.UseFileSystem(fileSystem =>
                {
                    fileSystem.BasePath = _testDirectoryPath;
                });
            });
        });
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        DirectoryHelper.DeleteIfExists(_testDirectoryPath, true);
    }
}