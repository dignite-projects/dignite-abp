using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings.DynamicForms;

[SettingDefinitionProviderName(TestSettingNames.TestSettingProviderName)]
public class TestSettingDefinitionFormProvider : SettingDefinitionFormProvider
{
    public override ILocalizableString DisplayName => new FixedLocalizableString(TestSettingNames.TestSettingProviderName);

    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(new FixedLocalizableString(TestSettingNames.TestSettingGroupName1), //add a group
                new SettingDefinition(TestSettingNames.TestSettingWithoutDefaultValue),
                new SettingDefinition(TestSettingNames.TestSettingWithDefaultValue, "default-value")
                    .UseTextboxForm(tb =>
                        {
                            tb.Required = true;
                            tb.Placeholder = "placeholder-text";
                            tb.CharLimit = 64;
                        }
                    ),
                new SettingDefinition(TestSettingNames.TestSettingEncrypted, isEncrypted: true)
        );
    }
}