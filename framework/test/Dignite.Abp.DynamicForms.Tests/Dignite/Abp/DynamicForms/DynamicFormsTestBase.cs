using Volo.Abp;
using Volo.Abp.Testing;

namespace Dignite.Abp.DynamicForms;

public class DynamicFormsTestBase : AbpIntegratedTest<DigniteAbpDynamicFormsTestModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}