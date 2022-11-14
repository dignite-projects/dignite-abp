using Dignite.Abp.FieldCustomizing.Forms;
using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing;

public interface ICustomizeFieldDefinition
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
    string FieldProviderName { get; set; }

    [NotNull]
    FormConfigurationDictionary Configuration { get; set; }
}