using Dignite.Abp.DynamicForms;

namespace Dignite.Abp.SettingManagement;

public class SettingDto : ICustomizeFieldInfo
{
    public SettingDto(
        string name,
        string displayName,
        string formProviderName,
        FormConfigurationDictionary formConfiguration,
        string description,
        string value)
    {
        Name = name;
        DisplayName = displayName;
        FormProviderName = formProviderName;
        FormConfiguration = formConfiguration;
        Description = description;
        Value = value;
    }

    public string Name { get; set; }
    public string DisplayName { get; set; }

    public string DefaultValue { get; set; }

    public string FormProviderName { get; set; }

    public FormConfigurationDictionary FormConfiguration { get; set; }

    public string Description { get; set; }
    public string Value { get; set; }
}