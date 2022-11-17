using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring;

public class FileTypeCheckHandlerConfiguration
{
    /// <summary>
    /// Limit the type of upload files
    /// </summary>
    public string[] AllowedFileTypeNames {
        get => _containerConfiguration.GetConfigurationOrDefault<string[]>(FileTypeCheckHandlerConfigurationNames.AllowedFileTypeNames, null);
        set => _containerConfiguration.SetConfiguration(FileTypeCheckHandlerConfigurationNames.AllowedFileTypeNames, value);
    }

    private readonly BlobContainerConfiguration _containerConfiguration;

    public FileTypeCheckHandlerConfiguration(BlobContainerConfiguration containerConfiguration)
    {
        _containerConfiguration = containerConfiguration;
    }
}