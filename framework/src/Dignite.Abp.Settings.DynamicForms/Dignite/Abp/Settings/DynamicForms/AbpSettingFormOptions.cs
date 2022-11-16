using Volo.Abp.Collections;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings.DynamicForms;

public class AbpSettingFormOptions : AbpSettingOptions
{
    public ITypeList<ISettingDefinitionFormProvider> GroupingDefinitionProviders { get; }

    public AbpSettingFormOptions() : base()
    {
        GroupingDefinitionProviders = new TypeList<ISettingDefinitionFormProvider>();
    }
}