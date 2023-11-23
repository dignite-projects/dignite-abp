using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Dignite.Abp.DataDictionary;

[DependsOn(
    typeof(AbpTestBaseModule),
    typeof(AbpAutofacModule),
    typeof(DigniteAbpDataDictionaryModule)
    )]
public class DigniteAbpDataDictionaryTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDataDictionaryOptions>(options =>
        {
            options.ValueProviders.Add<TestDataDictionaryValueProvider>();
        });
    }
}
