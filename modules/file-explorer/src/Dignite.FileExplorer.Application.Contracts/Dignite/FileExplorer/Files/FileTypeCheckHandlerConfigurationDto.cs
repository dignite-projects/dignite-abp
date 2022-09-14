namespace Dignite.FileExplorer.Files;

public class FileTypeCheckHandlerConfigurationDto
{
    public FileTypeCheckHandlerConfigurationDto(string[] allowedFileTypeNames)
    {
        AllowedFileTypeNames = allowedFileTypeNames;
    }

    /// <summary>
    /// 允许上传的文件类型
    /// </summary>
    public string[] AllowedFileTypeNames { get; }
}