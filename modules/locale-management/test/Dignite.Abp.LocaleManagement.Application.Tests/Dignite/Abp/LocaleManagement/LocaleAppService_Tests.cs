using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.Abp.LocaleManagement;

public abstract class LocaleAppService_Tests<TStartupModule> : LocaleManagementApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly ILocaleAppService _localeAppService;

    protected LocaleAppService_Tests()
    {
        _localeAppService = GetRequiredService<ILocaleAppService>();
    }

    [Fact]
    public async Task GetAsync()
    {
        var result = await _localeAppService.GetAsync();
        result.DefaultCultureName.ShouldBe("en-us");
    }

    [Fact]
    public async Task UpdateAsync()
    {
        var input = new UpdateLocaleInput();
        input.DefaultCultureName = "zh-cn";
        input.AvailableCultureNames = new string[] { "en-us", "zh-cn" };
        await _localeAppService.UpdateAsync(input);
        var result = await _localeAppService.GetAsync();
        result.DefaultCultureName.ShouldBe("zh-cn");
    }
}
