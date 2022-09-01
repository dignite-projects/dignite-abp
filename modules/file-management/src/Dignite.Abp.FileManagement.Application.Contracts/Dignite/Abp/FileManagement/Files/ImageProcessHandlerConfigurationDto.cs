namespace Dignite.Abp.FileManagement.Files;

public class ImageProcessHandlerConfigurationDto
{
    public ImageProcessHandlerConfigurationDto(int imageWidth, int imageHeight, bool imageSizeMustBeLargerThanPreset)
    {
        ImageWidth = imageWidth;
        ImageHeight = imageHeight;
        ImageSizeMustBeLargerThanPreset = imageSizeMustBeLargerThanPreset;
    }

    /// <summary>
    /// Scale the width of the image
    /// </summary>
    public int ImageWidth { get; }

    /// <summary>
    /// Scale the height of the image
    /// </summary>
    public int ImageHeight { get; }

    /// <summary>
    /// Whether allow uploaded image's size larger than preset size
    /// </summary>
    public bool ImageSizeMustBeLargerThanPreset { get; }
}
