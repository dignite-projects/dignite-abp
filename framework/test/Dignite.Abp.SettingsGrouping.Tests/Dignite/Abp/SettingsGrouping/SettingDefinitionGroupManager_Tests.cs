using System.Linq;
using Dignite.Abp.DynamicForms.Textbox;
using Shouldly;
using Xunit;

namespace Dignite.Abp.SettingsGrouping;

public class SettingDefinitionGroupManager_Tests : SettingsTestBase
{
    private readonly ISettingDefinitionGroupManager _settingDefinitionGroupManager;

    public SettingDefinitionGroupManager_Tests()
    {
        _settingDefinitionGroupManager = GetRequiredService<ISettingDefinitionGroupManager>();
    }

    [Fact]
    public void Should_Get_Test_Setting_Definition_AllGroups()
    {
        var groups = _settingDefinitionGroupManager.GetAllGroups();
        groups.ShouldNotBeEmpty();
    }

    [Fact]
    public void Should_Get_Test_Settings_Of_Group()
    {
        var definition = _settingDefinitionGroupManager.Get(TestSettingNames.TestSettingWithoutDefaultValue);
        definition.GetGroupOrNull().ShouldNotBeNull();
    }

    [Fact]
    public void Should_Get_Test_Setting_Definition_Dynamic_Form()
    {
        var allGroups = _settingDefinitionGroupManager.GetAllGroups();
        var group = allGroups.First(g=>g.Name== TestSettingNames.TestSettingGroupName);
        var subGroup = group.SubGroups.First(g => g.Name == TestSettingNames.TestSettingSubGroupName1);
        var setting1 = subGroup.SettingDefinitions.Single(sf => sf.Name == TestSettingNames.TestSettingWithDefaultValue);
        var fieldConfig = setting1.GetDynamicFormConfigurationOrNull();
        var textboxFormConfig = new TextboxConfiguration(fieldConfig);
        textboxFormConfig.Placeholder.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public void Should_Get_Test_Setting_Definition2_Dynamic_Form()
    {
        var allGroups = _settingDefinitionGroupManager.GetAllGroups();
        var group = allGroups.First(g => g.Name == TestSettingNames.TestSettingGroupName2);
        var subGroup = group.SubGroups.First(g => g.Name == TestSettingNames.TestSettingSubGroupName2);
        var setting1 = subGroup.SettingDefinitions.Single(sf => sf.Name == TestSettingNames.TestSettingPackager);
        var fieldConfig = setting1.GetDynamicFormConfigurationOrNull();
        var textboxFormConfig = new TextboxConfiguration(fieldConfig);
        textboxFormConfig.Placeholder.ShouldNotBeNullOrEmpty();
    }
}