using System;
using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing.Forms.Table;

[Serializable]
public class TableColumnFieldDefinition : ICustomizeFieldDefinition
{
    public TableColumnFieldDefinition()
    {
        this.Configuration = new FormConfigurationDictionary();
    }

    public TableColumnFieldDefinition(
        [NotNull] string name,
        [NotNull] string displayName,
        [NotNull] string fieldProviderName,
        [NotNull] string defaultValue,
        [NotNull] FormConfigurationDictionary configuration
        )
    {
        Name = name;
        DisplayName = displayName;
        FieldProviderName = fieldProviderName;
        DefaultValue = defaultValue;
        Configuration = configuration;
    }

    [NotNull]
    public string Name { get; set; }

    [NotNull]
    public string DisplayName { get; set; }

    /// <summary>
    /// The provider to be used to <see cref="IFormProvider.Name"/>
    /// </summary>
    [NotNull]
    public string FieldProviderName { get; set; }

    /// <summary>
    /// Default value of the field.
    /// </summary>
    [CanBeNull]
    public string DefaultValue { get; set; }

    [NotNull]
    public FormConfigurationDictionary Configuration { get; set; }
}