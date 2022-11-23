namespace Dignite.FileExplorer.Files;
public class FileHandlerConfigurationDto
{
    /// <summary>
    /// Limit file size(KB)
    /// </summary>
    public int MaxBlobSize { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string[] AllowedFileTypeNames { get; set; }


}
