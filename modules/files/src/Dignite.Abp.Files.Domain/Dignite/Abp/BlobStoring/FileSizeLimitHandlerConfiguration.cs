using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring;

public class FileSizeLimitHandlerConfiguration
{
    /// <summary>
    /// Limit file size(KB)
    /// </summary>
    public int MaxFileSize {
        get => _containerConfiguration.GetConfigurationOrDefault<int>(FileSizeLimitHandlerConfigurationNames.MaxFileSize);
        set => _containerConfiguration.SetConfiguration(FileSizeLimitHandlerConfigurationNames.MaxFileSize, value);
    }

    private readonly BlobContainerConfiguration _containerConfiguration;

    public FileSizeLimitHandlerConfiguration(BlobContainerConfiguration containerConfiguration)
    {
        _containerConfiguration = containerConfiguration;
    }
}