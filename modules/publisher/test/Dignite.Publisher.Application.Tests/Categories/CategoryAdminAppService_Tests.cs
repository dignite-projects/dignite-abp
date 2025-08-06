using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Publisher.Admin.Categories;
using Dignite.Publisher.Admin.Posts;
using Dignite.Publisher.Posts;
using Dignite.Publisher.TestBase;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.Publisher.Categories;

public abstract class CategoryAdminAppService_Tests<TStartupModule> : PublisherApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly ICategoryAdminAppService _categoryAdminAppService;
    private readonly PublisherTestData _testData;
    private readonly IPostAdminAppService _postAdminAppService;     

    protected CategoryAdminAppService_Tests()
    {
        _categoryAdminAppService = GetRequiredService<ICategoryAdminAppService>();
        _testData = GetRequiredService<PublisherTestData>();
        _postAdminAppService = GetRequiredService<IPostAdminAppService>();
    }

    [Fact]
    public async Task CreateAsync()
    {
        var createCategoryInput = new CreateCategoryInput
        {
            Locale = _testData.Local_En,
            ParentId = null,
            DisplayName = "Test Category",
            Name = "test-category",
            Description = "This is a test category.",
            IsActive = true,
            PostTypes = new List<string> { PostTypeConsts.ArticlePostTypeName },
            Order = 2
        };
        var result = await _categoryAdminAppService.CreateAsync(createCategoryInput);

        var newCategory = await _categoryAdminAppService.GetAsync(result.Id);
        newCategory.ShouldNotBeNull();
        newCategory.DisplayName.ShouldBe("Test Category");
        newCategory.PostTypes.Any(x => x == PostTypeConsts.ArticlePostTypeName).ShouldBeTrue();
    }

    [Fact]
    public async Task GetListAsync()
    {
        var result = await _categoryAdminAppService.GetListAsync(new GetCategoriesInput { 
            Locale = _testData.Local_En,
        });
        result.TotalCount.ShouldBe(1);
        result.Items.Any(x => x.Name == _testData.Category_1_Name).ShouldBeTrue();
    }

    [Fact]
    public async Task MoveAsync()
    {
        // Create a new category to move
        var createCategoryDto = new CreateCategoryInput
        {
            Locale = _testData.Local_En,
            ParentId = null,
            DisplayName = "Test Category",
            Name = "test-category",
            Description = "This is a test category.",
            IsActive = true,
            PostTypes = new List<string> { PostTypeConsts.ArticlePostTypeName },
            Order = 2
        };
        var category = await _categoryAdminAppService.CreateAsync(createCategoryDto);

        // Create a post to ensure the category can be moved with posts
        var createPostInput = new CreateArticlePostInput()
        {
            Locale = _testData.Local_En,
            Title = "Test Article Post",
            Slug = "test-post",
            CoverBlobName = "img.jpeg",
            Summary = "Test Article Post Summary",
            CategoryIds = new List<Guid> { category.Id },
            Content = "<p>Test Article Post Content</p>"
        };
        var post = await _postAdminAppService.CreateAsync(createPostInput);

        // Move the category to a new parent
        await _categoryAdminAppService.MoveAsync(category.Id, new MoveCategoryInput
        {
            ParentId = _testData.Category_1_Id,
            Order = 2
        });

        // Verify the category has been moved
        category = await _categoryAdminAppService.GetAsync(category.Id);
        category.ParentId.ShouldBe(_testData.Category_1_Id);

        // Verify the post is still associated with the category
        post = await _postAdminAppService.GetAsync(post.Id);
        post.PostCategories.Count.ShouldBe(2);
        post.PostCategories.ShouldContain(x => x.CategoryId == _testData.Category_1_Id);
    }

    [Fact]
    public async Task UpdateAsync()
    {
        var newName = "test-new-category";
        await _categoryAdminAppService.UpdateAsync(_testData.Category_1_Id, new UpdateCategoryInput
        {
            DisplayName = _testData.Category_1_DisplayName,
            Name = newName,
            Description = "Category Description",
            IsActive = false,
            PostTypes = new List<string> { PostTypeConsts.VideoPostTypeName }
        });

        var result = await _categoryAdminAppService.GetAsync(_testData.Category_1_Id);
        result.Name.ShouldBe(newName);
        result.PostTypes.Count.ShouldBe(1);
    }
}
