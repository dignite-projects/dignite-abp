namespace Dignite.Publisher.Posts;
public static class PostConsts
{
    public static int MaxTitleLength { get; set; } = 128;

    public static int MaxSlugLength { get; set; } = 256;

    public static int MinSlugLength { get; set; } = 8;

    /// <summary>
    /// Regular Expression of the Name property.
    /// </summary>
    public const string SlugRegularExpression = "^[a-zA-Z0-9_\\-\\.]+$";

    public static int MaxCoverBlobNameLength { get; set; } = 128;

    public static int MaxSummaryLength { get; set; } = 256;

    public static int MaxLocaleLength { get; set; } = 16;

    public const string EntityType = "PublisherPost";
}
