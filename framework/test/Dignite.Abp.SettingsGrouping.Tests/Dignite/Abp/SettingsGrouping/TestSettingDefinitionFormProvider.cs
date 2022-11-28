using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public class TestSettingDefinitionFormProvider : SettingDefinitionProvider, ISettingDefinitionGroupProvider, ITransientDependency
{
    public SettingDefinitionGroup Group => new SettingDefinitionGroup(TestSettingNames.TestSettingGroupName);

    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(new SettingDefinitionGroup(TestSettingNames.TestSettingSubGroupName1), 
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