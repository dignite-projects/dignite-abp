using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.DynamicForms.Textbox;

public class TextEditConfiguration : FormConfigurationBase
{
    public string Placeholder {
        get => ConfigurationDictionary.GetConfigurationOrDefault<string>(TextEditConfigurationNames.Placeholder, null);
        set => ConfigurationDictionary.SetConfiguration(TextEditConfigurationNames.Placeholder, value);
    }

    [Required]
    public TextEditMode Mode {
        get => ConfigurationDictionary.GetConfigurationOrDefault(TextEditConfigurationNames.Mode, TextEditMode.SingleLine);
        set => ConfigurationDictionary.SetConfiguration(TextEditConfigurationNames.Mode, value);
    }

    public int CharLimit {
        get => ConfigurationDictionary.GetConfigurationOrDefault(TextEditConfigurationNames.CharLimit, Mode == TextEditMode.SingleLine ? 256 : 1024);
        set => ConfigurationDictionary.SetConfiguration(TextEditConfigurationNames.CharLimit, value);
    }

    public TextEditConfiguration(FormConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }

    public TextEditConfiguration() : base()
    {
    }
}