using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public interface ISettingDefinitionGroupProvider : ISettingDefinitionProvider
{
    SettingDefinitionGroup Group { get; }
}