using System;
using System.Threading.Tasks;
using Dignite.Publisher.TestBase;
using Shouldly;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Modularity;
using Volo.CmsKit.Blogs;
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
    public async Task SlugExistsAsync_ShouldReturnTrue_WithExistingSlug()
    {
        var result = await _postRepository.SlugExistsAsync(_testData.Local_En, _testData.Post_1_Slug);

        result.ShouldBeTrue();
    }

    [Fact]
    public async Task SlugExistsAsync_ShouldReturnFalse_WithNonExistingSlug()
    {
        var nonExistingSlug = "any-other-url-slug";

        var result = await _postRepository.SlugExistsAsync(_testData.Local_En, nonExistingSlug);

        result.ShouldBeFalse();
    }

    [Fact]
    public async Task SlugExistsAsync_ShouldReturnFalse_WithNonExistingLanguage()
    {
        var result = await _postRepository.SlugExistsAsync("ja", _testData.Post_1_Slug);

        result.ShouldBeFalse();
    }

    [Fact]
    public async Task GetBySlugAsync_ShouldWorkProperly_WithCorrectParameters()
    {
        var post = await _postRepository.GetBySlugAsync(_testData.Local_En, _testData.Post_1_Slug);

        post.ShouldNotBeNull();
        post.Id.ShouldBe(_testData.Post_1_Id);
        post.Slug.ShouldBe(_testData.Post_1_Slug);
        post.ShouldBeOfType<ArticlePost>();
    }

    [Fact]
    public async Task GetBySlugAsync_ShouldHaveCreator_WithCorrectParameters()
    {
        var post = await _postRepository.GetBySlugAsync(_testData.Local_En, _testData.Post_1_Slug);

        post.ShouldNotBeNull();
        post.Id.ShouldBe(_testData.Post_1_Id);
        post.Slug.ShouldBe(_testData.Post_1_Slug);
        post.Creator.ShouldNotBeNull();
        post.Creator.Id.ShouldBe(_testData.User1Id);
    }

    [Fact]
    public async Task GetBySlugAsync_ShouldThrowException_WithNonExistingBlogPostSlug()
    {
        var nonExistingSlugUrl = "absolutely-non-existing-url";
        var exception = await Should.ThrowAsync<EntityNotFoundException>(
            async () => await _postRepository.GetBySlugAsync(_testData.Local_En, nonExistingSlugUrl));

        exception.ShouldNotBeNull();
        exception.EntityType.ShouldBe(typeof(Post));
    }

    [Fact]
    public async Task GetBySlugAsync_ShouldThrowException_WithNonExistingLanguage()
    {
        var exception = await Should.ThrowAsync<EntityNotFoundException>(
            async () => await _postRepository.GetBySlugAsync("ja", _testData.Post_1_Slug));

        exception.ShouldNotBeNull();
        exception.EntityType.ShouldBe(typeof(Post));
    }

    [Fact]
    public async Task GetPagedListAsync_ShouldWorkProperly_WithLanguage_WhileGetting10_WithoutSorting()
    {
        var result = await _postRepository.GetPagedListAsync(_testData.Local_En, [_testData.Category_1_Id]);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(2);
    }

    [Fact]
    public async Task GetPagedListAsync_ShouldHaveCreator_WithLanguage_WhileGetting10_WithoutSorting()
    {
        var result = await _postRepository.GetPagedListAsync(_testData.Local_En, [_testData.Category_1_Id]);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(2);

        result.ForEach(post => post.Creator.ShouldNotBeNull());
    }

    [Fact]
    public async Task GetPagedListAsync_ShouldWorkProperly_WithLanguage_WhileGetting1_WithoutSorting()
    {
        var result = await _postRepository.GetPagedListAsync(_testData.Local_En, [_testData.Category_1_Id], maxResultCount: 1);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(1);
    }

    [Fact]
    public async Task GetPagedListAsync_ShouldWorkProperly_WithLanguage_WhileGetting1InPage2_WithoutSorting()
    {
        var result = await _postRepository.GetPagedListAsync(_testData.Local_En, [_testData.Category_1_Id], skipCount: 1, maxResultCount: 1);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(1);
    }

    [Fact]
    public async Task GetPagedListAsync_ShouldWorkProperly_WithLanguage_WhileGetting10_WithSortingByTitle()
    {
        var result = await _postRepository.GetPagedListAsync(_testData.Local_En, [_testData.Category_1_Id], sorting: $"{nameof(BlogPost.Title)} asc");

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(2);
    }

    [Fact]
    public async Task GetCreatorsHasBlogPosts_ShouldWorkProperly()
    {
        var creators = await _postRepository.GetCreatorsHasPostsAsync(0, 100, null);

        creators.ShouldNotBeNull();
        creators.ShouldNotBeEmpty();
        creators.ShouldContain(x => x.Id == _testData.User1Id);
    }
}
