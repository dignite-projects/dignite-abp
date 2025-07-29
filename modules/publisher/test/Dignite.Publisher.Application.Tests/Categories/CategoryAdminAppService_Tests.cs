using System.Threading.Tasks;
using Dignite.Publisher.Admin.Categories;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.Publisher.Categories;

public abstract class CategoryAdminAppService_Tests<TStartupModule> : PublisherApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly ICategoryAdminAppService _categoryAdminAppService;

    protected CategoryAdminAppService_Tests()
    {
        _categoryAdminAppService = GetRequiredService<ICategoryAdminAppService>();
    }

    /*
    [Fact]
    public async Task GetAsync()
    {
        var result = await _sampleAppService.GetAsync();
        result.Value.ShouldBe(42);
    }

    [Fact]
    public async Task GetAuthorizedAsync()
    {
        var result = await _sampleAppService.GetAuthorizedAsync();
        result.Value.ShouldBe(42);
    }
    */
}
