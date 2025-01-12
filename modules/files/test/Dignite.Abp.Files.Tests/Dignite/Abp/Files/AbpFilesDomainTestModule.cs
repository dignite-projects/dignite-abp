using System;
using System.IO;
using Dignite.Abp.BlobStoring;
using Dignite.Abp.Files.TestObjects;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.IO;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Files;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpFilesDomainModule),
    typeof(AbpBlobStoringFileSystemModule)
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
        Configure<AbpBlobStoringOptions>(options =>
        {
            /*  
             *  The default configuration is set here, but it does not work. 
             *  Therefore, it is configured independently in each container.
             *  What caused it? No in-depth study. 
             *  Let's leave it to you. ^_^
             *  
            options.Containers.ConfigureDefault(container =>
            {
                container.UseFileSystem(fileSystem =>
                {
                        fileSystem.BasePath = _testDirectoryPath;
                });
            });
            */

            options.Containers
                .Configure<TestContainer2>(container =>
                {
                    container.UseFileSystem(fileSystem =>
                    {
                        fileSystem.BasePath = _testDirectoryPath;
                    });
                    container.AddFileSizeLimitHandler(config =>
                       config.MaxFileSize = 1
                    );
                })
                .Configure<TestContainer3>(container =>
                {
                    container.UseFileSystem(fileSystem =>
                    {
                        fileSystem.BasePath = _testDirectoryPath;
                    });
                    container.AddFileTypeCheckHandler(config =>
                       config.AllowedFileTypeNames = new string[] { ".jpeg" }
                    );
                });
        });
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        DirectoryHelper.DeleteIfExists(_testDirectoryPath, true);
    }
}
