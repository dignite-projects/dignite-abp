using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dignite.Publisher.Posts;
using Dignite.Publisher.TestBase;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.Publisher.Categories;

public abstract class CategoryManager_Tests<TStartupModule> : PublisherDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly CategoryManager _categoryManager;
    private readonly ICategoryRepository _categoryRepository;
    private readonly PublisherTestData _testData;
    private readonly IPostRepository _postRepository;

    public CategoryManager_Tests()
    {
        _categoryManager = GetRequiredService<CategoryManager>();
        _categoryRepository = GetRequiredService<ICategoryRepository>();
        _testData = GetRequiredService<PublisherTestData>();
        _postRepository = GetRequiredService<IPostRepository>();
    }

    [Fact]
    public async Task CategoryCreate_ShouldThrowException_WithExistName()
    {
        await Should.ThrowAsync<CategoryNameAlreadyExistException>(
            async () =>
            await _categoryManager.CreateAsync(
                _testData.Local_En, 
                _testData.Category_1_Id, 
                "Test Category",
                _testData.Category_2_Name, 
                "This is a test category.",
                true, 
                new List<string>() {
                    PostTypeConsts.ArticlePostTypeName,
                    PostTypeConsts.VideoPostTypeName
                }, 0)
            );
    }

    [Fact]
    public async Task CategoryCreate_ShouldThrowException_WithNotFound()
    {
        await Should.ThrowAsync<CategoryNotFoundException>(
            async () =>
            await _categoryManager.CreateAsync(
                _testData.Local_En,
                Guid.NewGuid(),
                "Test Category",
                _testData.Category_1_Name,
                "This is a test category.",
                true,
                new List<string>() {
                    PostTypeConsts.ArticlePostTypeName,
                    PostTypeConsts.VideoPostTypeName
                }, 0)
            );
    }

    [Fact]
    public async Task CategoryCreate_ShouldWorkProperly()
    {
        var category = await _categoryManager.CreateAsync(
            _testData.Local_En, null, "Test Category", "test-category", "This is a test category.", true,
            new List<string>() {
                PostTypeConsts.ArticlePostTypeName,
                PostTypeConsts.VideoPostTypeName
            }, 0
        );
        category = await _categoryRepository.InsertAsync(category, true);
        category.ShouldNotBeNull();
        category.DisplayName.ShouldBe("Test Category");
        category.Name.ShouldBe("test-category");
        category.Description.ShouldBe("This is a test category.");
        category.IsActive.ShouldBeTrue();
        category.PostTypes.Count.ShouldBe(2);
        category.PostTypes.ShouldContain(PostTypeConsts.ArticlePostTypeName);
        category.PostTypes.ShouldContain(PostTypeConsts.VideoPostTypeName);
    }

    [Fact]
    public async Task CategoryDelete_ShouldWorkProperly()
    {
        var category = await _categoryManager.CreateAsync(
            _testData.Local_En, null, "Test Category", "test-category", "This is a test category.", true, 
            new List<string>() { 
                PostTypeConsts.ArticlePostTypeName,
                PostTypeConsts.VideoPostTypeName
            }, 0
        );
        await _categoryRepository.InsertAsync(category, true);

        var childCategory = await _categoryManager.CreateAsync(
            _testData.Local_En, category.Id, "Test Category", "test-category", "This is a test category.", true,
            new List<string>() {
                PostTypeConsts.ArticlePostTypeName,
                PostTypeConsts.VideoPostTypeName
            }, 0
        );
        await _categoryRepository.InsertAsync(childCategory, true);

        await _categoryManager.DeleteAsync(category.Id);
        var deletedCategory = await _categoryRepository.FindAsync(category.Id, false);
        deletedCategory.ShouldBeNull();

        var deletedChildCategory = await _categoryRepository.FindAsync(childCategory.Id, false);
        deletedCategory.ShouldBeNull();
    }

    [Fact]
    public async Task CategoryGetTreeList_ShouldWorkProperly()
    {
        var categories = await _categoryManager.GetTreeListAsync(_testData.Local_En);
        categories.ShouldNotBeNull();
        categories.Count.ShouldBe(1);
        categories[0].DisplayName.ShouldBe(_testData.Category_1_DisplayName);
        categories[0].Children.ShouldNotBeNull();
        categories[0].Children.Count.ShouldBe(1);
        categories[0].Children[0].DisplayName.ShouldBe(_testData.Category_2_DisplayName);
    }


    [Fact]
    public async Task CategoryMove_ShouldThrowException_WithLocalMismatch()
    {
        var targetParentCategory = await _categoryRepository.GetAsync(_testData.Category_1_Id, false);
        var category = await _categoryManager.CreateAsync(
            null, null, "Test Category", "test-category", "This is a test category.", true,
            new List<string>() {
                PostTypeConsts.ArticlePostTypeName,
                PostTypeConsts.VideoPostTypeName
            }, 0
        );
        await _categoryRepository.InsertAsync(category, true);

        await Should.ThrowAsync<CategoryLocalMismatchException>(
            async () =>
            await _categoryManager.MoveAsync(category,targetParentCategory.Id, 2)
            );
    }

    [Fact]
    public async Task CategoryMove_ShouldWorkProperly()
    {
        var category = await _categoryManager.CreateAsync(
            _testData.Local_En, null, "Test Category", "test-category", "This is a test category.", true,
            new List<string>() {
                PostTypeConsts.ArticlePostTypeName,
                PostTypeConsts.VideoPostTypeName
            }, 0
        );
        await _categoryRepository.InsertAsync(category, true);

        await _categoryManager.MoveAsync(category, _testData.Category_1_Id, 2);
        await _categoryRepository.UpdateAsync(category, true);

        var categories = await _categoryManager.GetTreeListAsync(_testData.Local_En);

        categories[0].Children.Count.ShouldBe(2);
        categories[0].Children[1].DisplayName.ShouldBe("Test Category");
    }
}
