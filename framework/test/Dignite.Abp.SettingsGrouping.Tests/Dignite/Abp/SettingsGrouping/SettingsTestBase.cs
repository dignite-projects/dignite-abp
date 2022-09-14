using Volo.Abp;
using Volo.Abp.Testing;

namespace Dignite.Abp.SettingsGrouping;

public class SettingsTestBase : AbpIntegratedTest<AbpSettingsGroupingTestModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}