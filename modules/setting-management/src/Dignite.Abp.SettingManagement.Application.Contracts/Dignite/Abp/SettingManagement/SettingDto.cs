using Dignite.Abp.DynamicForms;

namespace Dignite.Abp.SettingManagement;

public class SettingDto : ICustomizeFieldInfo
{
    public SettingDto(
        string name,
        string displayName,
        string formName,
        FormConfigurationDictionary formConfiguration,
        string group,
        string description,
        string value)
    {
        Name = name;
        DisplayName = displayName;
        FormName = formName;
        FormConfiguration = formConfiguration;
        Group = group;
        Description = description;
        Value = value;
    }

    public string Name { get; set; }
    public string DisplayName { get; set; }

    public string DefaultValue { get; set; }

    public string FormName { get; set; }

    public FormConfigurationDictionary FormConfiguration { get; set; }

    public string Group { get; set; }
    public string Description { get; set; }
    public string Value { get; set; }
}