using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Settings;
using Xunit;

namespace Dignite.Abp.SettingsGrouping;

public class SettingProvider_Tests : SettingsTestBase
{
    private readonly ISettingProvider _settingProvider;

    public SettingProvider_Tests()
    {
        _settingProvider = GetRequiredService<ISettingProvider>();
    }

    [Fact]
    public async Task Should_Get_Test_Setting()
    {
        var setting1 = await _settingProvider.GetOrNullAsync(TestSettingNames.TestSettingWithDefaultValue);
        setting1.ShouldNotBeNull();
    }
}