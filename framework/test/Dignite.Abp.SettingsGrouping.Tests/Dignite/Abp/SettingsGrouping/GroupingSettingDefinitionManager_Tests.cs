using System.Linq;
using Dignite.Abp.FieldCustomizing.Forms.Textbox;
using Shouldly;
using Xunit;

namespace Dignite.Abp.SettingsGrouping;

public class GroupingSettingDefinitionManager_Tests : SettingsTestBase
{
    private readonly ISettingDefinitionGroupManager _settingDefinitionManager;

    public GroupingSettingDefinitionManager_Tests()
    {
        _settingDefinitionManager = GetRequiredService<ISettingDefinitionGroupManager>();
    }

    [Fact]
    public void Should_Get_Test_Setting_Definition_Provider()
    {
        var groups = _settingDefinitionManager.GetGroups();
        groups.ShouldNotBeEmpty();
    }

    [Fact]
    public void Should_Get_Test_Settings_Of_Group()
    {
        var sections = _settingDefinitionManager.GetSections(TestSettingNames.TestSettingGroupName);
        sections.ShouldNotBeNull();
    }

    [Fact]
    public void Should_Get_Test_Setting_Definition_Form()
    {
        var definitions = _settingDefinitionManager.GetList(TestSettingNames.TestSettingGroupName);
        var setting1 = definitions.Single(sf => sf.Name == TestSettingNames.TestSettingWithDefaultValue);
        var fieldConfig = setting1.GetControlConfigurationOrNull();
        var textboxFormConfig = new TextboxConfiguration(fieldConfig);
        textboxFormConfig.Placeholder.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public void Should_Get_Test_Setting_Definition2_Form()
    {
        var definitions = _settingDefinitionManager.GetList(TestSettingNames.TestSettingGroupName2);
        var setting1 = definitions.Single(sf => sf.Name == TestSettingNames.TestSettingPackager);
        var fieldConfig = setting1.GetControlConfigurationOrNull();
        var textboxFormConfig = new TextboxConfiguration(fieldConfig);
        textboxFormConfig.Placeholder.ShouldNotBeNullOrEmpty();
    }
}