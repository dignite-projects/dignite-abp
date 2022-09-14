using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring;

public class BlobSizeLimitHandlerConfiguration
{
    /// <summary>
    /// Limit file size(KB)
    /// </summary>
    public int MaximumBlobSize {
        get => _containerConfiguration.GetConfigurationOrDefault<int>(BlobSizeLimitHandlerConfigurationNames.MaximumBlobSize);
        set => _containerConfiguration.SetConfiguration(BlobSizeLimitHandlerConfigurationNames.MaximumBlobSize, value);
    }

    private readonly BlobContainerConfiguration _containerConfiguration;

    public BlobSizeLimitHandlerConfiguration(BlobContainerConfiguration containerConfiguration)
    {
        _containerConfiguration = containerConfiguration;
    }
}