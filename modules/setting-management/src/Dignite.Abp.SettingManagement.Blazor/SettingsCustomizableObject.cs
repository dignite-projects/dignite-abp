using Dignite.Abp.DynamicForms;

namespace Dignite.Abp.SettingManagement.Blazor;

public class SettingsCustomizableObject : IHasCustomFields
{
    public SettingsCustomizableObject()
    {
        this.CustomFields = new CustomFieldDictionary();
    }

    public CustomFieldDictionary CustomFields { get; }
}