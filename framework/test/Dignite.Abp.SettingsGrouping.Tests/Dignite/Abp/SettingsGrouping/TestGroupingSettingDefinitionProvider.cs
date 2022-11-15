using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public class TestGroupingSettingDefinitionProvider : SettingDefinitionGroupProvider
{
    public override string Section => TestSettingNames.TestSettingGroupName;

    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(new FixedLocalizableString(TestSettingNames.TestSettingSectionName1), //add a group
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
        );
    }
}