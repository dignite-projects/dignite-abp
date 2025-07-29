using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.Publisher.Categories;

public abstract class CategoryManager_Tests<TStartupModule> : PublisherDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    //private readonly SampleManager _sampleManager;

    public CategoryManager_Tests()
    {
        //_sampleManager = GetRequiredService<SampleManager>();
    }

    [Fact]
    public Task Method1Async()
    {
        return Task.CompletedTask;
    }
}
