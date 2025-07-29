using System.Threading.Tasks;
using Dignite.Publisher.TestBase;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.Publisher.Categories;

/* Write your custom repository tests like that, in this project, as abstract classes.
 * Then inherit these abstract classes from EF Core & MongoDB test projects.
 * In this way, both database providers are tests with the same set tests.
 */
public abstract class CategoryRepository_Tests<TStartupModule> : PublisherTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly PublisherTestData _testData;
    private readonly ICategoryRepository _categoryRepository;

    protected CategoryRepository_Tests()
    {
        _testData = GetRequiredService<PublisherTestData>();
        _categoryRepository = GetRequiredService<ICategoryRepository>();
    }


    [Fact]
    public async Task NameExistAsync_ShouldWorkProperly()
    {
        var exists = await _categoryRepository.NameExistsAsync(null, _testData.Category_1_Name);
        var notExists = await _categoryRepository.NameExistsAsync(null,"not-existing-category-name");

        exists.ShouldBeTrue();
        notExists.ShouldBeFalse();
    }
}
