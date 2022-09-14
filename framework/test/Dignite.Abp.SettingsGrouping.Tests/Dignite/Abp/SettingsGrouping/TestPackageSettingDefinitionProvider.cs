using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public class TestSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(TestSettingNames.TestSettingPackager, "abc")
        );
    }
}

public class TestPackageSettingDefinitionProvider : TestSettingDefinitionProvider, ISettingDefinitionGroupProvider, ITransientDependency
{
    public void Define(ISettingDefinitionGroupContext context)
    {
        var settings = new Dictionary<string, SettingDefinition>();
        base.Define(new SettingDefinitionContext(settings));

        context.Add(
            new SettingDefinitionGroup(TestSettingNames.TestSettingGroupName2),
            new SettingDefinitionSection(
                new FixedLocalizableString("testSection"),
                settings.Values.ToImmutableArray().ToArray()
            )
        );

        settings.GetValueOrDefault(TestSettingNames.TestSettingPackager)
            .UseTextboxControl(tb =>
                {
                    tb.Required = true;
                    tb.Placeholder = "placeholder-text";
                }
            );
    }
}