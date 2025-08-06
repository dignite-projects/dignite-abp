using System.Linq;
using System.Threading.Tasks;
using Dignite.Publisher.Public.Posts;
using Dignite.Publisher.TestBase;
using Shouldly;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.Publisher.Posts;

public abstract class PostPublicAppService_Tests<TStartupModule> : PublisherApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IPostPublicAppService _postPublicAppService;
    private readonly PublisherTestData _testData;

    protected PostPublicAppService_Tests()
    {
        _postPublicAppService = GetRequiredService<IPostPublicAppService>();
        _testData = GetRequiredService<PublisherTestData>();
    }

    [Fact]
    public async Task GetListAsync_ShouldWorkProperly_WithExistingBlog()
    {
        var posts = await _postPublicAppService.GetListAsync(new GetPostsInput { Locale = _testData.Local_En, MaxResultCount = 2 });

        posts.ShouldNotBeNull();
        posts.TotalCount.ShouldBe(2);
        posts.Items.ShouldNotBeEmpty();
        posts.Items.Count.ShouldBe(2);
        posts.Items.Any(x => x.PostType == PostTypeConsts.ArticlePostTypeName).ShouldBeTrue();
        posts.Items.Any(x => x.PostType == PostTypeConsts.VideoPostTypeName).ShouldBeTrue();
        posts.Items.Any(x => x.GetType() == typeof(ArticlePostDto)).ShouldBeTrue();
        posts.Items.Any(x => x.GetType() == typeof(VideoPostDto)).ShouldBeTrue();
    }

    [Fact]
    public async Task GetAsync_ShouldWorkProperly_WithExistingSlug()
    {
        var post = await _postPublicAppService.GetAsync(_testData.Local_En, _testData.Post_1_Slug);

        post.Id.ShouldBe(_testData.Post_1_Id);
        post.Title.ShouldBe(_testData.Post_1_Title);
        post.GetType().ShouldBe(typeof(ArticlePostDto));
    }

    [Fact]
    public async Task GetAsync_ShouldThrowException_WithNonExistingBlogPostSlug()
    {
        var nonExistingSlug = "any-other-url";
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
                            await _postPublicAppService.GetAsync(_testData.Local_En, nonExistingSlug));

        exception.EntityType.ShouldBe(typeof(Post));
    }

    [Fact]
    public async Task GetListAsync_ShouldFilterByUser()
    {
        //should get all not filtered by user
        var posts = await _postPublicAppService.GetListAsync(
            new GetPostsInput { Locale = _testData.Local_En, MaxResultCount = 2 }
            );

        posts.ShouldNotBeNull();
        posts.TotalCount.ShouldBe(2);
        posts.Items.ShouldNotBeEmpty();
        posts.Items.Count.ShouldBe(2);

        //should get only one filtered by user
        posts = await _postPublicAppService.GetListAsync(
            new GetPostsInput { Locale = _testData.Local_En, CreatorId=_testData.User1Id, MaxResultCount = 2 }
            );

        posts.ShouldNotBeNull();
        posts.TotalCount.ShouldBe(2);
        posts.Items.ShouldNotBeEmpty();
        posts.Items.Count.ShouldBe(2);
    }
}
