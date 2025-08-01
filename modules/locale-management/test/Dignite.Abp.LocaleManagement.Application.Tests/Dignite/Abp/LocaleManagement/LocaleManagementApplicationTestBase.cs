using Volo.Abp.Modularity;

namespace Dignite.Abp.LocaleManagement;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class LocaleManagementApplicationTestBase<TStartupModule> : LocaleManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
