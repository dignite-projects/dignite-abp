using Volo.Abp.Modularity;

namespace Dignite.Abp.TenantDomains;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class TenantDomainsApplicationTestBase<TStartupModule> : TenantDomainsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
