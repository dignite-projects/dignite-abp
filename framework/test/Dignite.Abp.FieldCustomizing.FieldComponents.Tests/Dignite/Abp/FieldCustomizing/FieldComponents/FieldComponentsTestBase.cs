using Volo.Abp;
using Volo.Abp.Testing;

namespace Dignite.Abp.FieldCustomizing.FieldComponents;

public class FieldComponentsTestBase : AbpIntegratedTest<AbpFieldCustomizingFieldComponentsTestModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}