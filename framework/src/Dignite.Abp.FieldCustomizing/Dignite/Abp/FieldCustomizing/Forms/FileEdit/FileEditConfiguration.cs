using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.Forms.Upload;

public class FileEditConfiguration : FormConfigurationBase
{
    public string Placeholder {
        get => ConfigurationDictionary.GetConfigurationOrDefault<string>(FileEditConfigurationNames.Placeholder, null);
        set => ConfigurationDictionary.SetConfiguration(FileEditConfigurationNames.Placeholder, value);
    }

    [Required]
    public bool Multiple {
        get => ConfigurationDictionary.GetConfigurationOrDefault(FileEditConfigurationNames.Multiple, false);
        set => ConfigurationDictionary.SetConfiguration(FileEditConfigurationNames.Multiple, value);
    }

    [Required]
    public string Filter {
        get => ConfigurationDictionary.GetConfigurationOrDefault<string>(FileEditConfigurationNames.Filter);
        set => ConfigurationDictionary.SetConfiguration(FileEditConfigurationNames.Filter, value);
    }

    public FileEditConfiguration(FormConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }

    public FileEditConfiguration() : base()
    {
    }
}