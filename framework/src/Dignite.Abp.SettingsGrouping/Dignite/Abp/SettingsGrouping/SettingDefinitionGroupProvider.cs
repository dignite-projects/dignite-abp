using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public abstract class SettingDefinitionGroupProvider : SettingDefinitionProvider, ISettingDefinitionGroupProvider, ITransientDependency
{
    public override void Define(ISettingDefinitionContext context)
    {
        var settings = new Dictionary<string, SettingDefinition>();
        Define(new SettingDefinitionGroupContext(settings));

        context.Add(
            settings.Values.ToImmutableList().ToArray()
        );
    }

    public abstract void Define(ISettingDefinitionGroupContext context);
}