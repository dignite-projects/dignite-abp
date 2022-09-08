
namespace Dignite.Abp.FieldCustomizing;

public class CustomizeFieldDefinitionConsts
{

    /// <summary>
    /// Maximum length of the <see cref="ICustomizeFieldDefinition.Name"/> property.
    /// Default value: 64
    /// </summary>
    public static int MaxNameLength = 64;

    /// <summary>
    /// Regular Expression of the <see cref="ICustomizeFieldDefinition.Name"/> property.
    /// </summary>
    public static string NameRegularExpression = "^[a-zA-Z][A-Za-z0-9_-]+$";


    /// <summary>
    /// Maximum length of the <see cref="ICustomizeFieldDefinition.DisplayName"/> property.
    /// Default value: 64
    /// </summary>
    public static int MaxDisplayNameLength = 64;

    /// <summary>
    /// Maximum length of the <see cref="ICustomizeFieldDefinition.FieldProviderName"/> property.
    /// Default value: 128
    /// </summary>
    public static int MaxFieldProviderNameLength = 128;


}
