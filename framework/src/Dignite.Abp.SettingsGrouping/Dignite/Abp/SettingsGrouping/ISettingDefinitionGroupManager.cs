using System.Collections.Generic;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public interface ISettingDefinitionGroupManager : ISettingDefinitionManager
{
    /// <summary>
    /// Get setting definition groups
    /// </summary>
    /// <returns></returns>
    IReadOnlyList<SettingDefinitionGroup> GetGroups();

    /// <summary>
    /// Get setting definition list
    /// </summary>
    /// <param name="groupName"></param>
    /// <returns></returns>
    IReadOnlyList<SettingDefinition> GetList(string groupName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="groupName"></param>
    /// <returns></returns>
    IReadOnlyList<SettingDefinitionSection> GetSections(string groupName);
}
