using Volo.Abp.Localization;

namespace Dignite.Abp.SettingsGrouping;

public class TestGroupingSettingDefinitionProvider : SettingDefinitionGroupProvider
{
    public override void Define(ISettingDefinitionGroupContext context)
    {
        context.Add(
            new SettingDefinitionGroup(TestSettingNames.TestSettingGroupName, null), //add a group
            new SettingDefinitionSection(
                new FixedLocalizableString("testSection"), // add a setting items section
                new Volo.Abp.Settings.SettingDefinition(TestSettingNames.TestSettingWithoutDefaultValue),
                new Volo.Abp.Settings.SettingDefinition(TestSettingNames.TestSettingWithDefaultValue, "default-value")
                    .UseTextboxControl(tb =>
                        {
                            tb.Required = true;
                            tb.Placeholder = "placeholder-text";
                            tb.CharLimit = 64;
                        }
                    ),
                new Volo.Abp.Settings.SettingDefinition(TestSettingNames.TestSettingEncrypted, isEncrypted: true)
            )
        );
    }
}