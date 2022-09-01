using Volo.Abp;
using Volo.Abp.Testing;

namespace Dignite.Abp.FieldCustomizing;

public class FieldCustomizingTestBase : AbpIntegratedTest<AbpBlobStoringTestModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

}
