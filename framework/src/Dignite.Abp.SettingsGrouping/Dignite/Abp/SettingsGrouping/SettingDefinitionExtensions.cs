using System.Collections.Generic;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public static class SettingDefinitionExtensions
{

    /// <summary>
    /// Gets the section name of the setting
    /// </summary>
    /// <param name="setting"></param>
    /// <returns></returns>
    public static ILocalizableString GetSectionOrNull(
        this SettingDefinition setting)
    {
        var group = setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.SectionName);
        if (group != null)
        {
            return (ILocalizableString)group;
        }
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="setting"></param>
    /// <param name="sectionName"></param>
    public static SettingDefinition SetSection(
        this SettingDefinition setting,
        ILocalizableString sectionName)
    {
        setting.WithProperty(SettingDefinitionPropertiesNames.SectionName, sectionName);
        return setting;
    }
}
