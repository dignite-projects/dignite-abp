using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public class SettingDefinitionGroupContext : SettingDefinitionContext, ISettingDefinitionGroupContext
{
    public SettingDefinitionGroupContext(Dictionary<string, SettingDefinition> settings)
        : base(settings)
    {
    }

    /// <summary>
    /// Group of settings
    /// </summary>
    public SettingDefinitionGroup Group { get; set; }

    public void Add(
        [NotNull] SettingDefinitionGroup group,
        [NotNull] params SettingDefinitionSection[] definitions)
    {
        Group = group;
        foreach (var section in definitions)
        {
            if (section.Section != null)
            {
                foreach (var definition in section.Definitions)
                {
                    definition.SetSection(section.Section);
                }
            }

            Add(section.Definitions);
        }
    }
}