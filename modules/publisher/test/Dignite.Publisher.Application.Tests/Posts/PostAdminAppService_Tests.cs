using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Publisher.Admin.Posts;
using Dignite.Publisher.Categories;
using Dignite.Publisher.TestBase;
using Shouldly;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Modularity;
using Volo.CmsKit.Admin.Blogs;
using Volo.CmsKit.Blogs;
using Xunit;

namespace Dignite.Publisher.Posts;

public abstract class PostAdminAppService_Tests<TStartupModule> : PublisherApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IPostAdminAppService _postAdminAppService;
    private readonly PublisherTestData _testData;

    protected PostAdminAppService_Tests()
    {
        _postAdminAppService = GetRequiredService<IPostAdminAppService>();
        _testData = GetRequiredService<PublisherTestData>();
    }

    [Fact]
    public async Task CreateAsync_ShouldWorkProperly_WithCorrectData()
    {
        var createPostInput = new CreateArticlePostInput()
        {
            Locale = _testData.Local_En,
            Title = "Test Article Post",
            Slug = "test-post",
            CoverBlobName = "img.jpeg",
            Summary = "Test Article Post Summary",
            CategoryIds = new List<Guid> { _testData.Category_2_Id },
            Content = "<p>Test Article Post Content</p>"
        };
        var result = await _postAdminAppService.CreateAsync(createPostInput);

        var newPost = await _postAdminAppService.GetAsync(result.Id);
        newPost.ShouldNotBeNull();
        newPost.Slug.ShouldBe("test-post");
        newPost.PostCategories.Any(x => x.CategoryId == _testData.Category_2_Id).ShouldBeTrue();
    }

    [Fact]
    public async Task GetAsync_ShouldWorkProperly_WithExistingId()
    {
        var result = await _postAdminAppService.GetAsync(_testData.Post_1_Id);
        result.ShouldNotBeNull();
        result.GetType().ShouldBe(typeof(ArticlePostAdminDto));
        result.CreatorId.ShouldBe(_testData.User1Id);
    }

    [Fact]
    public async Task GetAsync_ShouldThrowException_WithNonExistingId()
    {
        var nonExistingId = Guid.NewGuid();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
                            await _postAdminAppService.GetAsync(nonExistingId));

        exception.EntityType.ShouldBe(typeof(Post));
        exception.Id.ShouldBe(nonExistingId);
    }

    [Fact]
    public async Task GetListAsync_ShouldWorkProperly_WithDefaultParameters()
    {
        var result = await _postAdminAppService.GetListAsync(new GetPostsInput
        {
            Locale = _testData.Local_En
        });
        result.TotalCount.ShouldBe(2);
        result.Items.Single(x => x.Slug == _testData.Post_1_Slug).PostType.ShouldBe(PostTypeConsts.ArticlePostTypeName);
        result.Items.Single(x => x.Slug == _testData.Post_2_Slug).PostType.ShouldBe(PostTypeConsts.VideoPostTypeName);
    }


    [Fact]
    public async Task UpdateAsync_ShouldWorkProperly_WithRegularDatas()
    {
        var newSlug = "test-new-Post";
        await _postAdminAppService.UpdateAsync(_testData.Post_1_Id, new UpdateArticlePostInput
        {
            Locale = _testData.Local_En,
            Title = _testData.Post_1_Title,
            Slug = newSlug,
            CoverBlobName = "new-img.jpeg",
            Summary = "Test Article Post Summary",
            CategoryIds = new List<Guid> { _testData.Category_2_Id },
            Content = "<p>Test Article Post Content</p>"
        });

        var result = await _postAdminAppService.GetAsync(_testData.Post_1_Id);
        result.Slug.ShouldBe(newSlug);
        result.PostCategories.Count.ShouldBe(1);
    }


    [Fact]
    public async Task UpdateAsync_ShouldThrowException_WhileUpdatingWithAlreadyExistingSlug()
    { 
        var exception = await Should.ThrowAsync<PostSlugAlreadyExistException>(async () =>
                            await _postAdminAppService.UpdateAsync(_testData.Post_1_Id, new UpdateArticlePostInput
                            {
                                Locale = _testData.Local_En,
                                Title = _testData.Post_1_Title,
                                Slug = _testData.Post_2_Slug,
                                CoverBlobName = "new-img.jpeg",
                                Summary = "Test Article Post Summary",
                                CategoryIds = new List<Guid> { _testData.Category_2_Id },
                                Content = "<p>Test Article Post Content</p>"
                            }));

        exception.Slug.ShouldBe(_testData.Post_2_Slug);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_WhileUpdatingWithNonExistentCategory()
    {
        var newCategoryId = Guid.NewGuid();
        var exception = await Should.ThrowAsync<CategoryNotFoundException>(async () =>
                            await _postAdminAppService.UpdateAsync(_testData.Post_1_Id, new UpdateArticlePostInput
                            {
                                Locale = _testData.Local_En,
                                Title = _testData.Post_1_Title,
                                Slug = _testData.Post_1_Slug,
                                CoverBlobName = "new-img.jpeg",
                                Summary = "Test Article Post Summary",
                                CategoryIds = new List<Guid> { _testData.Category_2_Id, newCategoryId },
                                Content = "<p>Test Article Post Content</p>"
                            }));

        exception.CategoryId.ShouldBe(newCategoryId);
    }

    [Fact]
    public async Task DeleteAsync_ShouldWorkProperly_WithExistingId()
    {
        await _postAdminAppService.DeleteAsync(_testData.Post_1_Id);

        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
                            await _postAdminAppService.GetAsync(_testData.Post_1_Id));

        exception.EntityType.ShouldBe(typeof(Post));
        exception.Id.ShouldBe(_testData.Post_1_Id);
    }

    [Fact]
    public async Task PublishAsync_ShouldWorkProperly()
    {
        var newPost = await CreateArticlePost();

        await _postAdminAppService.PublishAsync(newPost.Id);
        var post = await _postAdminAppService.GetAsync(newPost.Id);
        post.Status.ShouldBe(PostStatus.Published);
    }

    [Fact]
    public async Task DraftAsync_ShouldWorkProperly()
    {
        var newPost = await CreateArticlePost();
        newPost.Status.ShouldBe(PostStatus.Draft);

        await _postAdminAppService.DraftAsync(newPost.Id);
        var post = await _postAdminAppService.GetAsync(newPost.Id);
        post.Status.ShouldBe(PostStatus.Draft);
    }

    [Fact]
    public async Task ArchiveAsync_ShouldWorkProperly()
    {
        var newPost = await CreateArticlePost();
        newPost.Status.ShouldBe(PostStatus.Draft);

        await _postAdminAppService.ArchiveAsync(newPost.Id);
        var post = await _postAdminAppService.GetAsync(newPost.Id);
        post.Status.ShouldBe(PostStatus.Archived);
    }

    private async Task<PostDtoBase> CreateArticlePost()
    {
        var createPostInput = new CreateArticlePostInput()
        {
            Locale = _testData.Local_En,
            Title = "Test Article Post",
            Slug = "test-post",
            CoverBlobName = "img.jpeg",
            Summary = "Test Article Post Summary",
            CategoryIds = new List<Guid> { _testData.Category_2_Id },
            Content = "<p>Test Article Post Content</p>"
        };
        var result = await _postAdminAppService.CreateAsync(createPostInput);

        return await _postAdminAppService.GetAsync(result.Id);
    }
}
