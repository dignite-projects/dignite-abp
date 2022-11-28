using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms;

/// <summary>
/// Customize field info;
/// One form is applied to one field. It is the carrier of the form
/// </summary>
public interface ICustomizeFieldInfo
{
    /// <summary>
    /// Field name
    /// </summary>
    /// <remarks>
    /// To ensure that it is unique within the scope of the field set
    /// </remarks>
    [NotNull]
    string Name { get; set; }

    /// <summary>
    /// Field display name
    /// </summary>
    [NotNull]
    string DisplayName { get; set; }

    /// <summary>
    /// Default value of the field.
    /// </summary>
    [CanBeNull]
    string DefaultValue { get; set; }

    /// <summary>
    /// Form used by field
    /// </summary>
    [NotNull]
    string FormProviderName { get; }

    /// <summary>
    /// Form configuration of field
    /// </summary>
    [NotNull]
    FormConfigurationDictionary FormConfiguration { get; }
}