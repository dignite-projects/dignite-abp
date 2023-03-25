namespace Dignite.Abp.DynamicForms;

public static class CustomizeFieldInfoConsts
{
    /// <summary>
    /// Maximum length of the <see cref="ICustomizeFieldInfo.Name"/> property.
    /// Default value: 64
    /// </summary>
    public static int MaxNameLength { get; set; } = 64;

    /// <summary>
    /// Regular Expression of the <see cref="ICustomizeFieldInfo.Name"/> property.
    /// </summary>
    public const string NameRegularExpression = "^[a-zA-Z][A-Za-z0-9_-]+$";

    /// <summary>
    /// Maximum length of the <see cref="ICustomizeFieldInfo.DisplayName"/> property.
    /// Default value: 64
    /// </summary>
    public static int MaxDisplayNameLength { get; set; } = 64;

    /// <summary>
    /// Maximum length of the <see cref="ICustomizeFieldInfo.FormName"/> property.
    /// Default value: 128
    /// </summary>
    public static int MaxFormNameLength { get; set; } = 64;
}