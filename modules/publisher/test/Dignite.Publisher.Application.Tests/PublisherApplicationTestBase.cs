using Volo.Abp.Modularity;

namespace Dignite.Publisher;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class PublisherApplicationTestBase<TStartupModule> : PublisherTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
