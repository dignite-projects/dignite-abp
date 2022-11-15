using Dignite.Abp.DynamicForms;
namespace Dignite.Abp.FieldCustomizing;

public class CustomizeFieldDefinitionConsts
{
    /// <summary>
    /// Maximum length of the <see cref="ICustomizeField.Name"/> property.
    /// Default value: 64
    /// </summary>
    public static int MaxNameLength = 64;

    /// <summary>
    /// Regular Expression of the <see cref="ICustomizeField.Name"/> property.
    /// </summary>
    public static string NameRegularExpression = "^[a-zA-Z][A-Za-z0-9_-]+$";

    /// <summary>
    /// Maximum length of the <see cref="ICustomizeField.DisplayName"/> property.
    /// Default value: 64
    /// </summary>
    public static int MaxDisplayNameLength = 64;

    /// <summary>
    /// Maximum length of the <see cref="ICustomizeField.FormProviderName"/> property.
    /// Default value: 128
    /// </summary>
    public static int MaxFormProviderNameLength = 256;
}