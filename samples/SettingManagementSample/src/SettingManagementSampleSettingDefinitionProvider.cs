using System.Collections.Immutable;
using Dignite.Abp.SettingsGrouping;
using SettingManagementSample.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Settings;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace SettingManagementSample;

public class SettingManagementSampleSettingDefinitionProvider: AbpIdentitySettingDefinitionProvider, ISettingDefinitionGroupProvider, ITransientDependency
{
    public void Define(ISettingDefinitionGroupContext context)
    {
        var settings = new Dictionary<string, SettingDefinition>();
        base.Define(new SettingDefinitionContext(settings));


        //Package existing settings
        context.Add(
            new SettingDefinitionGroup(TestSettingNames.TestSettingGroupName, L("SettingsGroup")),
            new SettingDefinitionSection(
                L("SettingsSection"),
                settings.Values.ToImmutableArray().ToArray()
            )
        );
        settings.GetValueOrDefault(IdentitySettingNames.Password.RequiredLength)
            .UseTextboxControl(tb =>
            {
                tb.Required = true;
                tb.Placeholder = "placeholder-text";
            }
            );


        //Add new setting definition item
        context.Add(
            new SettingDefinitionGroup(TestSettingNames.TestSettingGroupName, L("SettingsGroup")),
            new SettingDefinitionSection(
                L("SettingsSection2"),
                new SettingDefinition(TestSettingNames.TestSettingWithoutDefaultValue),
                new SettingDefinition(TestSettingNames.TestSettingWithDefaultValue, "default-value",L("SettingName1"),L("SettingDescription1"))
                    .UseTextboxControl(tb =>
                    {
                        tb.Required = true;
                        tb.Placeholder = "placeholder-text";
                        tb.CharLimit = 64;
                    }
                    ),
                new SettingDefinition(TestSettingNames.TestSettingEncrypted, isEncrypted: true)
            )
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SettingManagementSampleResource>(name);
    }
}
