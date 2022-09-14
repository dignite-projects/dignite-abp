using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public class SettingDefinitionSection
{
    public SettingDefinitionSection(ILocalizableString section = null, params SettingDefinition[] definitions)
    {
        Section = section;
        Definitions = definitions;
    }

    public ILocalizableString Section { get; private set; }

    public SettingDefinition[] Definitions { get; private set; }
}