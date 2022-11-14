using Dignite.Abp.FieldCustomizing;
using Dignite.Abp.FieldCustomizing.Forms;

namespace Dignite.Abp.SettingManagement;

public class SettingDto : ICustomizeFieldDefinition
{
    public SettingDto(
        string section,
        string name,
        string displayName,
        string description,
        string value,
        string fieldProviderName,
        FormConfigurationDictionary configuration)
    {
        Section = section;
        Name = name;
        DisplayName = displayName;
        Description = description;
        Value = value;
        FieldProviderName = fieldProviderName;
        Configuration = configuration;
    }

    public string Section { get; set; }

    public string Name { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }

    public string Value { get; set; }

    public string DefaultValue { get; set; }

    public string FieldProviderName { get; set; }

    public FormConfigurationDictionary Configuration { get; set; }
}