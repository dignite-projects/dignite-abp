using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms;

/// <summary>
/// Custom filed
/// </summary>
public interface ICustomizeField
{
    /// <summary>
    /// Unique name
    /// </summary>
    [NotNull]
    string Name { get; set; }

    [NotNull]
    string DisplayName { get; set; }

    /// <summary>
    /// Default value of the field.
    /// </summary>
    [CanBeNull]
    string DefaultValue { get; set; }

    [NotNull]
    string FormProviderName { get; }

    [NotNull]
    FormConfigurationDictionary FormConfiguration { get; }
}