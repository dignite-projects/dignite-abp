using System.Collections.Generic;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public interface ISettingDefinitionGroupManager : ISettingDefinitionManager
{
    /// <summary>
    /// Get setting definition groups
    /// </summary>
    /// <returns></returns>
    IReadOnlyList<string> GetGroups();

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
    IReadOnlyList<ILocalizableString> GetSections(string groupName);
}