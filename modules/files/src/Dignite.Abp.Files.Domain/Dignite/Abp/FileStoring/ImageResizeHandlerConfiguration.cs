using Volo.Abp.BlobStoring;

namespace Dignite.Abp.FileStoring;

public class ImageResizeHandlerConfiguration
{
    /// <summary>
    /// Scale the width of the image
    /// </summary>
    public int ImageWidth {
        get => _containerConfiguration.GetConfigurationOrDefault<int>(ImageResizeHandlerConfigurationNames.ImageWidth);
        set => _containerConfiguration.SetConfiguration(ImageResizeHandlerConfigurationNames.ImageWidth, value);
    }

    /// <summary>
    /// Scale the height of the image
    /// </summary>
    public int ImageHeight {
        get => _containerConfiguration.GetConfigurationOrDefault<int>(ImageResizeHandlerConfigurationNames.ImageHeight);
        set => _containerConfiguration.SetConfiguration(ImageResizeHandlerConfigurationNames.ImageHeight, value);
    }

    /// <summary>
    /// Whether allow uploaded image's size larger than preset size
    /// </summary>
    public bool ImageSizeMustBeLargerThanPreset {
        get => _containerConfiguration.GetConfigurationOrDefault<bool>(ImageResizeHandlerConfigurationNames.ImageSizeMustBeLargerThanPreset, false);
        set => _containerConfiguration.SetConfiguration(ImageResizeHandlerConfigurationNames.ImageSizeMustBeLargerThanPreset, value);
    }

    private readonly BlobContainerConfiguration _containerConfiguration;

    public ImageResizeHandlerConfiguration(BlobContainerConfiguration containerConfiguration)
    {
        _containerConfiguration = containerConfiguration;
    }
}
