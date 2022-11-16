using Volo.Abp.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings.DynamicForms;

public abstract class SettingDefinitionFormProvider : SettingDefinitionProvider, ISettingDefinitionFormProvider, ITransientDependency
{
    public virtual ILocalizableString DisplayName { get; protected set; }
}