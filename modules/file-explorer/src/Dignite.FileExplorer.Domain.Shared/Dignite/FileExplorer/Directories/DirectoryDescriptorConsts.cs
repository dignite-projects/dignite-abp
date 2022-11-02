namespace Dignite.FileExplorer.Directories;

public static class DirectoryDescriptorConsts
{
    /// <summary>
    /// Default value: 256
    /// </summary>
    public static int MaxNameLength { get; set; } = 64;

    /// <summary>
    /// Regular Expression of the Name property.
    /// </summary>
    public const string NameRegularExpression = "^[A-Za-z0-9_.-]+$";
}