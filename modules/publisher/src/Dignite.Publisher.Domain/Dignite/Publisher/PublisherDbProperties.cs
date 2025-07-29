namespace Dignite.Publisher;

public static class PublisherDbProperties
{
    public static string DbTablePrefix { get; set; } = "pbl_";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Publisher";
}
