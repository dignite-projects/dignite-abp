using System;
using System.Collections.Generic;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;
public static class SettingDefinitionContextExtensions
{
    public static void Add(
        this ISettingDefinitionContext context, ILocalizableString groupName, params SettingDefinition[] definitions)
    {
        if (definitions.IsNullOrEmpty())
        {
            return;
        }

        
                foreach (var definition in definitions)
                {
                    definition.SetSection(groupName);
                }

        context.Add(definitions);
    }
}
