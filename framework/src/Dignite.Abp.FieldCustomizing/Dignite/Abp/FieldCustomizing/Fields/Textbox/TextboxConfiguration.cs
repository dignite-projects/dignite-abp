using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.Fields.Textbox;

public class TextboxConfiguration : FieldConfigurationBase
{
    public string Placeholder {
        get => ConfigurationDictionary.GetConfigurationOrDefault<string>(TextboxConfigurationNames.Placeholder, null);
        set => ConfigurationDictionary.SetConfiguration(TextboxConfigurationNames.Placeholder, value);
    }

    [Required]
    public TextboxMode Mode {
        get => ConfigurationDictionary.GetConfigurationOrDefault(TextboxConfigurationNames.Mode, TextboxMode.SingleLine);
        set => ConfigurationDictionary.SetConfiguration(TextboxConfigurationNames.Mode, value);
    }

    public int CharLimit {
        get => ConfigurationDictionary.GetConfigurationOrDefault(TextboxConfigurationNames.CharLimit, Mode == TextboxMode.SingleLine ? 256 : 1024);
        set => ConfigurationDictionary.SetConfiguration(TextboxConfigurationNames.CharLimit, value);
    }

    public TextboxConfiguration(FieldConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }

    public TextboxConfiguration() : base()
    {
    }
}