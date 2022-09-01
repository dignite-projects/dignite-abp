using Dignite.Abp.BlobStoring.InfoPersistent.Fakes;
using Dignite.Abp.BlobStoring.InfoPersistent.TestObjects;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.BlobStoring;
using Volo.Abp.Modularity;


namespace Dignite.Abp.BlobStoring.InfoPersistent;

[DependsOn(
    typeof(AbpBlobStoringInfoPersistentModule),
    typeof(AbpTestBaseModule),
    typeof(AbpAutofacModule)
    )]
public class AbpBlobStoringInfoPersistentTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IBlobProvider>(Substitute.For<FakeBlobProvider1>());

        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers
                .ConfigureDefault(container =>
                {
                    container.ProviderType = typeof(FakeBlobProvider1);
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
}
