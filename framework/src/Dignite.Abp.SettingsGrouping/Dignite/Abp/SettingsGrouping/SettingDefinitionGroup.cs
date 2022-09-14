using System.Collections.Generic;
using System.Collections.Immutable;
using Volo.Abp;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

/// <summary>
/// Group of setting definition
/// </summary>
public class SettingDefinitionGroup
{
    public SettingDefinitionGroup(string name, ILocalizableString displayName = null)
    {
        Name = name;
        DisplayName = displayName ?? new FixedLocalizableString(name);
    }

    /// <summary>
    /// Group name
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///
    /// </summary>
    private ILocalizableString _displayName;

    /// <summary>
    ///
    /// </summary>
    public ILocalizableString DisplayName {
        get => _displayName;
        set => _displayName = Check.NotNull(value, nameof(value));
    }

    public IReadOnlyList<SettingDefinition> SettingDefinitions { get; private set; }

    internal void IncludeSettingDefinitions(Dictionary<string, SettingDefinition> settings)
    {
        SettingDefinitions = settings.Values.ToImmutableList();
    }
}