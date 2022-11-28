using System.Collections.Generic;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

/// <summary>
/// 
/// </summary>
public class SettingDefinitionGroup
{
    public SettingDefinitionGroup(string name, ILocalizableString displayName = null, ILocalizableString description = null, string icon = null)
    {
        Name = name;
        DisplayName = displayName;
        Description = description;
        Icon = icon;
    }

    public string Name { get; protected set; }

    public ILocalizableString DisplayName { get; protected set; }

    public ILocalizableString Description { get; protected set; }

    public string Icon { get; protected set; }

    public IReadOnlyList<SettingDefinitionGroup> SubGroups { get; internal set; }

    public IReadOnlyList<SettingDefinition> SettingDefinitions { get; internal set; }
}
