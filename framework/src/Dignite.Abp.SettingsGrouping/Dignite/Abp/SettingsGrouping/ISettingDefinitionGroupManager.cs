using System.Collections.Generic;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public interface ISettingDefinitionGroupManager : ISettingDefinitionManager
{
    IReadOnlyList<SettingDefinitionGroup> GetAllGroups();
}