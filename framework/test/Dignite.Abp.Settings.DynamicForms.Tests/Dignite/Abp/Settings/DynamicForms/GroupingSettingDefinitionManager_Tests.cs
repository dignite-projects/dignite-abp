using System.Linq;
using Dignite.Abp.DynamicForms.Textbox;
using Shouldly;
using Xunit;

namespace Dignite.Abp.Settings.DynamicForms;

public class GroupingSettingDefinitionManager_Tests : SettingsTestBase
{
    private readonly ISettingDefinitionFormManager _settingDefinitionManager;

    public GroupingSettingDefinitionManager_Tests()
    {
        _settingDefinitionManager = GetRequiredService<ISettingDefinitionFormManager>();
    }

    [Fact]
    public void Should_Get_Test_Setting_Definition_Provider()
    {
        var groups = _settingDefinitionManager.GetProviders();
        groups.ShouldNotBeEmpty();
    }

    [Fact]
    public void Should_Get_Test_Settings_Of_Group()
    {
        var definition = _settingDefinitionManager.Get(TestSettingNames.TestSettingWithoutDefaultValue);
        definition.GetGroupOrNull().ShouldNotBeNull();
    }

    [Fact]
    public void Should_Get_Test_Setting_Definition_Form()
    {
        var definitions = _settingDefinitionManager.GetList(TestSettingNames.TestSettingProviderName);
        var setting1 = definitions.Single(sf => sf.Name == TestSettingNames.TestSettingWithDefaultValue);
        var fieldConfig = setting1.GetFormConfigurationOrNull();
        var textboxFormConfig = new TextboxConfiguration(fieldConfig);
        textboxFormConfig.Placeholder.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public void Should_Get_Test_Setting_Definition2_Form()
    {
        var definitions = _settingDefinitionManager.GetList(TestSettingNames.TestSettingProviderName2);
        var setting1 = definitions.Single(sf => sf.Name == TestSettingNames.TestSettingPackager);
        var fieldConfig = setting1.GetFormConfigurationOrNull();
        var textboxFormConfig = new TextboxConfiguration(fieldConfig);
        textboxFormConfig.Placeholder.ShouldNotBeNullOrEmpty();
    }
}