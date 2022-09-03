namespace Dignite.FileExplorer;

public static class FileExplorerDbProperties
{
    public static string DbTablePrefix { get; set; } = "FileExplorer";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "FileExplorer";
}
