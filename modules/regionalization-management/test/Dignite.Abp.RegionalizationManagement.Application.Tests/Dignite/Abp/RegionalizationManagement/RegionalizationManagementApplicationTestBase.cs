using Volo.Abp.Modularity;

namespace Dignite.Abp.RegionalizationManagement;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class RegionalizationManagementApplicationTestBase<TStartupModule> : RegionalizationManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
