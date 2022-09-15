using Volo.Abp;
using Volo.Abp.Testing;

namespace Dignite.Abp.Files;

public abstract class AbpFilesDomainTestBase : AbpIntegratedTest<AbpFilesDomainTestModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
