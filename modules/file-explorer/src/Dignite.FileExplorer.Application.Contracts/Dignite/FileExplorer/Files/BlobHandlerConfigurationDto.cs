namespace Dignite.FileExplorer.Files;
public class BlobHandlerConfigurationDto
{
    /// <summary>
    /// Limit file size(KB)
    /// </summary>
    public int MaximumBlobSize { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string[] AllowedFileTypeNames { get; set; }
}
