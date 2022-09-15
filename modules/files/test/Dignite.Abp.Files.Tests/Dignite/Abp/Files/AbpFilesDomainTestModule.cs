using System;
using System.IO;
using Dignite.Abp.Files.Fakes;
using Dignite.Abp.Files.TestObjects;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.IO;
using Volo.Abp.Modularity;
using Dignite.Abp.BlobStoring;

namespace Dignite.Abp.Files;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpBlobStoringFileSystemModule),
    typeof(AbpFilesDomainModule)
    )]
public class AbpFilesDomainTestModule : AbpModule
{
    private readonly string _testDirectoryPath;

    public AbpFilesDomainTestModule()
    {
        _testDirectoryPath = Path.Combine(
            Path.GetTempPath(),
            Guid.NewGuid().ToString("N")
        );
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IFileStore<FakeFile>>(Substitute.For<FakeFileStore>());
        context.Services.AddSingleton<FileManager<FakeFile,FakeFileStore>>(Substitute.For<FakeFileManager>());




        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureAll((containerName, containerConfiguration) =>
                {
                    containerConfiguration.UseFileSystem(fileSystem =>
                    {
                        fileSystem.BasePath = _testDirectoryPath;
                    });
                })
                .Configure<TestContainer2>(container =>
                {
                    container.AddBlobSizeLimitHandler(config =>
                       config.MaximumBlobSize = 1
                    );
                })
                .Configure<TestContainer3>(container =>
                {
                    container.AddFileTypeCheckHandler(config =>
                       config.AllowedFileTypeNames = new string[] { ".jpeg" }
                    );
                })
                .Configure<TestContainer4>(container =>
                {
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
