namespace Dignite.FileExplorer.Files;

public static class FileDescriptorConsts
{
    public static int MaxEntityTypeLength = 128;

    public static int MaxEntityIdLength = 128;

    /// <summary>
    /// Regular Expression of the EntityType property.
    /// </summary>
    public const string EntityTypeRegularExpression = "^[A-Za-z0-9_.-]+$";
}