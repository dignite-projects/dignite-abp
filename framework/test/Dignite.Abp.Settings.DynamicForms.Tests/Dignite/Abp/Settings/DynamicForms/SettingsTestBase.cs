using Volo.Abp;
using Volo.Abp.Testing;

namespace Dignite.Abp.Settings.DynamicForms;

public class SettingsTestBase : AbpIntegratedTest<AbpSettingsGroupingTestModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}