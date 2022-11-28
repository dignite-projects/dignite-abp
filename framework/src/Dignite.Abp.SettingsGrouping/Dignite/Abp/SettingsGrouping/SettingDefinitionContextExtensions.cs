using System;
using System.Collections.Generic;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;
public static class SettingDefinitionContextExtensions
{
    public static void Add(
        this ISettingDefinitionContext context, SettingDefinitionGroup group, params SettingDefinition[] definitions)
    {
        if (definitions.IsNullOrEmpty())
        {
            return;
        }


        foreach (var definition in definitions)
        {
            definition.SetGroup(group);
        }

        context.Add(definitions);
    }
}
