using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace Dignite.CmsKit;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpTestBaseModule),
    typeof(AbpAuthorizationModule),
    typeof(DigniteCmsKitDomainModule)
    )]
public class CmsKitTestBaseModule : AbpModule
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            GlobalFeatureManager.Instance.Modules.DigniteCmsKit().EnableAll();
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
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
