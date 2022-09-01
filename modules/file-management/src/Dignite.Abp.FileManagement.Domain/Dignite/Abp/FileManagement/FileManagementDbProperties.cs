namespace Dignite.Abp.FileManagement;

public static class FileManagementDbProperties
{
    public static string DbTablePrefix { get; set; } = "fm";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "DigniteFileManagement";
}
