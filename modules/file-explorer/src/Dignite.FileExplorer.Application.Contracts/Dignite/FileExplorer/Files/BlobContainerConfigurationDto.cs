namespace Dignite.FileExplorer.Files;

public class BlobContainerConfigurationDto
{
    public BlobContainerConfigurationDto(BlobSizeLimitHandlerConfigurationDto blobSizeLimitHandlerConfiguration, FileTypeCheckHandlerConfigurationDto fileTypeCheckHandlerConfiguration, ImageProcessHandlerConfigurationDto imageProcessHandlerConfiguration)
    {
        BlobSizeLimitHandlerConfiguration = blobSizeLimitHandlerConfiguration;
        FileTypeCheckHandlerConfiguration = fileTypeCheckHandlerConfiguration;
        ImageProcessHandlerConfiguration = imageProcessHandlerConfiguration;
    }

    public BlobSizeLimitHandlerConfigurationDto BlobSizeLimitHandlerConfiguration { get; }
    public FileTypeCheckHandlerConfigurationDto FileTypeCheckHandlerConfiguration { get; }
    public ImageProcessHandlerConfigurationDto ImageProcessHandlerConfiguration { get; }
}