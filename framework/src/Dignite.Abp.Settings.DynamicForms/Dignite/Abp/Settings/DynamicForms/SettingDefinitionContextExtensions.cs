using System;
using System.Collections.Generic;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings.DynamicForms;
public static class SettingDefinitionContextExtensions
{
    public static void Add(
        this ISettingDefinitionContext context, ILocalizableString group, params SettingDefinition[] definitions)
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
