using System.Collections.Generic;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

/// <summary>
/// Group of setting definition
/// </summary>
public class SettingDefinitionGroup
{
    public SettingDefinitionGroup(string name, Dictionary<string, SettingDefinition> definitions)
    {
        Name = name;
        Definitions=definitions;
    }

    /// <summary>
    /// Group name
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///
    /// </summary>
    public Dictionary<string, SettingDefinition> Definitions { get; }


}