using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.BlobStoring;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace Dignite.FileExplorer;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpTestBaseModule),
    typeof(AbpAuthorizationModule),
    typeof(FileExplorerDomainModule)
    )]
public class FileExplorerTestBaseModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IBlobProvider>(Substitute.For<FakeBlobProvider>());

        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureAll((containerName, containerConfiguration) =>
            {
                containerConfiguration.ProviderType = typeof(FakeBlobProvider);
            });
        });

        context.Services.AddAlwaysAllowAuthorization();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        SeedTestData(context);
    }

    private static void SeedTestData(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(async () =>
        {
            using (var scope = context.ServiceProvider.CreateScope())
            {
                await scope.ServiceProvider
                    .GetRequiredService<IDataSeeder>()
                    .SeedAsync();
            }
        });
    }
}