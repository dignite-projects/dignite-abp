using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings.DynamicForms;

public interface ISettingDefinitionFormProvider : ISettingDefinitionProvider
{
    ILocalizableString DisplayName { get; }
}