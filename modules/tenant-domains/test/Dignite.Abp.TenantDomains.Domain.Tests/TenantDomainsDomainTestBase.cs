using Volo.Abp.Modularity;

namespace Dignite.Abp.TenantDomains;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class TenantDomainsDomainTestBase<TStartupModule> : TenantDomainsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
