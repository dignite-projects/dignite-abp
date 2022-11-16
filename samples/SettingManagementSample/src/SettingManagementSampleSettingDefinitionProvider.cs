using System.Collections.Immutable;
using Dignite.Abp.Settings.DynamicForms;
using SettingManagementSample.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Settings;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace SettingManagementSample;

[SettingDefinitionProviderName(TestSettingNames.TestSettingProviderName)]
public class SettingManagementSampleSettingDefinitionProvider: AbpIdentitySettingDefinitionProvider, ISettingDefinitionFormProvider, ITransientDependency
{
    public ILocalizableString DisplayName => L("SettingsGroup");

    public override void Define(ISettingDefinitionContext context)
    {
        var settings = new Dictionary<string, SettingDefinition>();
        base.Define(new SettingDefinitionContext(settings));

        settings.GetValueOrDefault(IdentitySettingNames.Password.RequiredLength)
            .UseTextboxForm(tb =>
            {
                tb.Required = true;
                tb.Placeholder = "placeholder-text";
            }
            );

        //Add inherited settings
        context.Add(
            L(TestSettingNames.TestSettingGroupName),
            settings.Values.ToImmutableArray().ToArray()
        );

        //Add new setting definition item
        context.Add(L(TestSettingNames.TestSettingGroupName2),
            new SettingDefinition(TestSettingNames.TestSettingWithoutDefaultValue)
                .UseTextboxForm(tb =>
                {
                    tb.Required = true;
                    tb.Placeholder = "placeholder-text";
                    tb.CharLimit = 64;
                }),
            new SettingDefinition(TestSettingNames.TestSettingWithDefaultValue, "default-value", L("SettingName1"), L("SettingDescription1"))
                .UseTextboxForm(tb =>
                {
                    tb.Required = true;
                    tb.Placeholder = "placeholder-text";
                    tb.CharLimit = 64;
                })
        );

        //Add Ungrouped Settings
        context.Add(
            new SettingDefinition(TestSettingNames.TestSettingEncrypted, isEncrypted: true)
                .UseTextboxForm(tb =>
                {
                    tb.Required = true;
                    tb.Placeholder = "placeholder-text";
                    tb.CharLimit = 64;
                })
        );
    }


    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SettingManagementSampleResource>(name);
    }
}
