using Volo.Abp;
using Volo.Abp.Testing;

namespace Dignite.Abp.Points;

public class PointsTestBase : AbpIntegratedTest<DigniteAbpPointsTestModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}