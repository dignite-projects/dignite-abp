using Dignite.Abp.SettingsGrouping;
using SettingManagementSample.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace SettingManagementSample;

public class SettingManagementSampleSettingDefinitionProvider: SettingDefinitionProvider, ISettingDefinitionGroupProvider, ITransientDependency
{
    public SettingDefinitionGroup Group => new SettingDefinitionGroup(
        TestSettingNames.TestSettingGroupName, 
        L(TestSettingNames.TestSettingGroupName), 
        L("Setting_Group_Description"),
        "fas fa fa-certificate"
        );

    public override void Define(ISettingDefinitionContext context)
    {
        //Add new setting definition item
        context.Add(
            new SettingDefinitionGroup(TestSettingNames.TestSettingSubGroupName2, L(TestSettingNames.TestSettingSubGroupName2), L("Setting_SubGroup_Description2")),
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
                    tb.Description = "这里是文本框的输入提示！";
                    tb.CharLimit = 64;
                })
        );
    }


    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SettingManagementSampleResource>(name);
    }
}
