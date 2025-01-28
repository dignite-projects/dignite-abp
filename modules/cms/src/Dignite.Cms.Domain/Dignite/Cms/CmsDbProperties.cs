namespace Dignite.Cms;

public static class CmsDbProperties
{
    public static string DbTablePrefix { get; set; } = "Cms";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Cms";
}
