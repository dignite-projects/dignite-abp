using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.DynamicForms.FileExplorer;

public class FileExplorerConfiguration : FormConfigurationBase
{
    public string FileContainerName {
        get => ConfigurationDictionary.GetConfiguration<string>(FileExplorerConfigurationNames.FileContainerName, null);
        set => ConfigurationDictionary.SetConfiguration(FileExplorerConfigurationNames.FileContainerName, value);
    }

    [Required]
    public bool UploadFileMultiple {
        get => ConfigurationDictionary.GetConfiguration(FileExplorerConfigurationNames.UploadFileMultiple, false);
        set => ConfigurationDictionary.SetConfiguration(FileExplorerConfigurationNames.UploadFileMultiple, value);
    }

    public FileExplorerConfiguration(FormConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }

    public FileExplorerConfiguration() : base()
    {
    }
}