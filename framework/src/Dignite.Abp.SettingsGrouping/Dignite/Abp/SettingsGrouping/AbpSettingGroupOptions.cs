using Dignite.Abp.SettingsGrouping;
using Volo.Abp.Collections;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public class AbpSettingGroupOptions : AbpSettingOptions
{
    public ITypeList<ISettingDefinitionGroupProvider> GroupingDefinitionProviders { get; }

    public AbpSettingGroupOptions() : base()
    {
        GroupingDefinitionProviders = new TypeList<ISettingDefinitionGroupProvider>();
    }
}
