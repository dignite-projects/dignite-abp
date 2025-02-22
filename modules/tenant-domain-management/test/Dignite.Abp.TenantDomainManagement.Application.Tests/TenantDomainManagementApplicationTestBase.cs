using Volo.Abp.Modularity;

namespace Dignite.Abp.TenantDomainManagement;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class TenantDomainManagementApplicationTestBase<TStartupModule> : TenantDomainManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
