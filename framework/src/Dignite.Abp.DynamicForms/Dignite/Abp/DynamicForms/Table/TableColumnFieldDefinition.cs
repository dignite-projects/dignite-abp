using System;
using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms.Table;

[Serializable]
public class TableColumnCustomField : ICustomizeField
{
    public TableColumnCustomField()
    {
        this.FormConfiguration = new FormConfigurationDictionary();
    }

    public TableColumnCustomField(
        [NotNull] string name,
        [NotNull] string displayName,
        [NotNull] string fieldProviderName,
        [NotNull] string defaultValue,
        [NotNull] FormConfigurationDictionary configuration
        )
    {
        Name = name;
        DisplayName = displayName;
        FormProviderName = fieldProviderName;
        DefaultValue = defaultValue;
        FormConfiguration = configuration;
    }

    [NotNull]
    public string Name { get; set; }

    [NotNull]
    public string DisplayName { get; set; }

    /// <summary>
    /// The provider to be used to <see cref="IFormProvider.Name"/>
    /// </summary>
    [NotNull]
    public string FormProviderName { get; set; }

    /// <summary>
    /// Default value of the field.
    /// </summary>
    [CanBeNull]
    public string DefaultValue { get; set; }

    [NotNull]
    public FormConfigurationDictionary FormConfiguration { get; set; }
}