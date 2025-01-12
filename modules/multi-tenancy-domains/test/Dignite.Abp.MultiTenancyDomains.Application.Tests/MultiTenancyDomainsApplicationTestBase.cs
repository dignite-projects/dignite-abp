using Volo.Abp.Modularity;

namespace Dignite.Abp.MultiTenancyDomains;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class MultiTenancyDomainsApplicationTestBase<TStartupModule> : MultiTenancyDomainsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
