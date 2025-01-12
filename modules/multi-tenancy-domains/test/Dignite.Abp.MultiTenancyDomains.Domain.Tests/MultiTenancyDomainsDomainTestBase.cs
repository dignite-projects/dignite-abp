using Volo.Abp.Modularity;

namespace Dignite.Abp.MultiTenancyDomains;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class MultiTenancyDomainsDomainTestBase<TStartupModule> : MultiTenancyDomainsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
