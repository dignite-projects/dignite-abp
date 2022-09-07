using System;
using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing.Fields.Table;

[Serializable]
public class TableColumnFieldDefinition: ICustomizeFieldDefinition
{
    public TableColumnFieldDefinition()
    {
        this.Configuration = new FieldConfigurationDictionary();
    }

    public TableColumnFieldDefinition(
        [NotNull] string name,
        [NotNull] string displayName,
        [NotNull] string fieldProviderName,
        [NotNull] string defaultValue,
        [NotNull] FieldConfigurationDictionary configuration
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
    /// The provider to be used to <see cref="IFieldProvider.Name"/>
    /// </summary>
    [NotNull]
    public string FieldProviderName { get; set; }


    /// <summary>
    /// Default value of the field.
    /// </summary>
    [CanBeNull]
    public string DefaultValue { get; set; }

    [NotNull]
    public FieldConfigurationDictionary Configuration { get; set; }
}
