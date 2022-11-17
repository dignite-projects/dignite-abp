﻿using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Settings;
using Xunit;

namespace Dignite.Abp.Settings.DynamicForms;

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
        var allSettings = await _settingProvider.GetAllAsync();
        var setting1 = await _settingProvider.GetOrNullAsync(TestSettingNames.TestSettingWithDefaultValue);
        setting1.ShouldNotBeNull();
    }
}