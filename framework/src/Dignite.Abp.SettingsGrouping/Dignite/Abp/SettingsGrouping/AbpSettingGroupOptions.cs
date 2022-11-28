using Volo.Abp.Collections;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public class AbpSettingGroupOptions : AbpSettingOptions
{
    public ITypeList<ISettingDefinitionGroupProvider> DefinitionGroupProviders { get; }

    public AbpSettingGroupOptions() : base()
    {
        DefinitionGroupProviders = new TypeList<ISettingDefinitionGroupProvider>();
    }
}