using Volo.Abp;
using Volo.Abp.Testing;

namespace Dignite.Abp.DynamicForms.Components;

public class FieldComponentsTestBase : AbpIntegratedTest<AbpDynamicFormsComponentsTestModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}