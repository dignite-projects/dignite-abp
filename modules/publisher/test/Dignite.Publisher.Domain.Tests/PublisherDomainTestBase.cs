using Volo.Abp.Modularity;

namespace Dignite.Publisher;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class PublisherDomainTestBase<TStartupModule> : PublisherTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
