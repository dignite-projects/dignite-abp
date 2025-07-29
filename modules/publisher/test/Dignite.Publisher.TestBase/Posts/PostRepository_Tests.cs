using System.Reflection.Metadata;
using System.Threading.Tasks;
using Dignite.Publisher.TestBase;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.Publisher.Posts;

/* Write your custom repository tests like that, in this project, as abstract classes.
 * Then inherit these abstract classes from EF Core & MongoDB test projects.
 * In this way, both database providers are tests with the same set tests.
 */
public abstract class PostRepository_Tests<TStartupModule> : PublisherTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly PublisherTestData _testData;
    private readonly IPostRepository _postRepository;

    protected PostRepository_Tests()
    {
        _testData = GetRequiredService<PublisherTestData>();
        _postRepository = GetRequiredService<IPostRepository>();
    }


    [Fact]
    public async Task SlugExistAsync_ShouldWorkProperly()
    {
        var exists = await _postRepository.SlugExistsAsync(_testData.Local_En, _testData.Post_1_Slug);
        var notExists = await _postRepository.SlugExistsAsync(_testData.Local_En, "not-existing-post-slug");

        exists.ShouldBeTrue();
        notExists.ShouldBeFalse();
    }

    [Fact]
    public async Task FindBySlugAsync_ShouldWorkProperly_WithExistingSlug()
    {
        var post = await _postRepository.FindBySlugAsync(_testData.Local_En, _testData.Post_1_Slug);

        post.ShouldNotBeNull();
        post.Id.ShouldBe(_testData.Post_1_Id);
        post.Title.ShouldBe(_testData.Post_1_Title);
        post.ShouldBeOfType<ArticlePost>();
    }
}
