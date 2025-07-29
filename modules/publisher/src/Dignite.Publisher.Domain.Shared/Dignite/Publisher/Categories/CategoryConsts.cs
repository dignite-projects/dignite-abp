namespace Dignite.Publisher.Categories;
public static class CategoryConsts
{
    public static int MaxDisplayNameLength { get; set; } = 64;

    public static int MaxNameLength { get; set; } = 64;

    /// <summary>
    /// Regular Expression of the Name property.
    /// </summary>
    public const string NameRegularExpression = "^[a-zA-Z0-9_\\-\\. ]+$";

    public static int MaxLocalLength { get; set; } = 16;
    public static int MaxDescriptionLength { get; set; } = 256;
}
