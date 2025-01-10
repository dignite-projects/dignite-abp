using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.Abp.RegionalizationManagement;

public abstract class RegionalizationAppService_Tests<TStartupModule> : RegionalizationManagementApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IRegionalizationAppService _regionalizationAppService;

    protected RegionalizationAppService_Tests()
    {
        _regionalizationAppService = GetRequiredService<IRegionalizationAppService>();
    }

    [Fact]
    public async Task GetAsync()
    {
        var result = await _regionalizationAppService.GetAsync();
        result.DefaultCultureName.ShouldBe("en-us");
    }

    [Fact]
    public async Task UpdateAsync()
    {
        var input = new UpdateRegionalizationInput();
        input.DefaultCultureName = "zh-cn";
        input.AvailableCultureNames = new string[] { "en-us", "zh-cn" };
        await _regionalizationAppService.UpdateAsync(input);
        var result = await _regionalizationAppService.GetAsync();
        result.DefaultCultureName.ShouldBe("zh-cn");
    }
}
