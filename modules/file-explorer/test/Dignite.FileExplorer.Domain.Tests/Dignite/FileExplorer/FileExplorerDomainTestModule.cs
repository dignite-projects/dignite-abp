using System;
using System.IO;
using Dignite.Abp.BlobStoring;
using Dignite.Abp.Files.TestObjects;
using Dignite.FileExplorer.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.IO;
using Volo.Abp.Modularity;

namespace Dignite.FileExplorer;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */

[DependsOn(
    typeof(FileExplorerEntityFrameworkCoreTestModule),
    typeof(AbpBlobStoringFileSystemModule)
    )]
public class FileExplorerDomainTestModule : AbpModule
{
    private readonly string _testDirectoryPath;

    public FileExplorerDomainTestModule()
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
                    container.UseFileSystem(fileSystem =>
                    {
                        fileSystem.BasePath = _testDirectoryPath;
                    });
                    container.AddImageResizeHandler(imageResize =>
                    {
                        imageResize.ImageWidth = 200;
                        imageResize.ImageHeight = 200;
                        imageResize.ImageSizeMustBeLargerThanPreset = false;
                    });
                });
        });
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        DirectoryHelper.DeleteIfExists(_testDirectoryPath, true);
    }
}