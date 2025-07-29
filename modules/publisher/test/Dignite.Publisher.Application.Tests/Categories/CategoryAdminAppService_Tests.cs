using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Publisher.Admin.Categories;
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

    protected CategoryAdminAppService_Tests()
    {
        _categoryAdminAppService = GetRequiredService<ICategoryAdminAppService>();
        _testData = GetRequiredService<PublisherTestData>();
    }

    [Fact]
    public async Task CreateAsync()
    {
        var createCategoryDto = new CreateCategoryDto
        {
            Local = "en",
            ParentId = null,
            DisplayName = "Test Category",
            Name = "test-category",
            Description = "This is a test category.",
            IsActive = true,
            PostTypes = new List<string> { PostTypeConsts.ArticlePostTypeName },
            Order = 2
        };
        var result = await _categoryAdminAppService.CreateAsync(createCategoryDto);

        var newCategory = await _categoryAdminAppService.GetAsync(result.Id);
        newCategory.ShouldNotBeNull();
        newCategory.DisplayName.ShouldBe("Test Category");
        newCategory.PostTypes.Any(x => x == PostTypeConsts.ArticlePostTypeName).ShouldBeTrue();
    }

    [Fact]
    public async Task GetListAsync()
    {
        var result = await _categoryAdminAppService.GetListAsync(new GetCategoriesInput { 
            Local = "en",
        });
        result.TotalCount.ShouldBe(1);
        result.Items.Any(x => x.Name == _testData.Category_1_Name).ShouldBeTrue();
    }

    [Fact]
    public async Task MoveAsync()
    {
        var createCategoryDto = new CreateCategoryDto
        {
            Local = _testData.Local_En,
            ParentId = null,
            DisplayName = "Test Category",
            Name = "test-category",
            Description = "This is a test category.",
            IsActive = true,
            PostTypes = new List<string> { PostTypeConsts.ArticlePostTypeName },
            Order = 2
        };
        var newCategory = await _categoryAdminAppService.CreateAsync(createCategoryDto);

        await _categoryAdminAppService.MoveAsync(newCategory.Id, new MoveCategoryInput
        {
            ParentId = _testData.Category_1_Id,
            Order = 2
        });

        var result = await _categoryAdminAppService.GetAsync(newCategory.Id);
        result.ParentId.ShouldBe(_testData.Category_1_Id);
    }

    [Fact]
    public async Task UpdateAsync()
    {
        var newName = "test-new-category";
        await _categoryAdminAppService.UpdateAsync(_testData.Category_1_Id, new UpdateCategoryDto
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
