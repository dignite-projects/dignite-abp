using System;
using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms.Table;

[Serializable]
public class TableColumnCustomField : ICustomizeFieldInfo
{
    public TableColumnCustomField()
    {
        this.FormConfiguration = new FormConfigurationDictionary();
    }

    public TableColumnCustomField(
        [NotNull] string name,
        [NotNull] string displayName,
        [NotNull] string fieldName,
        [NotNull] string defaultValue,
        [NotNull] FormConfigurationDictionary configuration
        )
    {
        Name = name;
        DisplayName = displayName;
        FormName = fieldName;
        DefaultValue = defaultValue;
        FormConfiguration = configuration;
    }

    [NotNull]
    public string Name { get; set; }

    [NotNull]
    public string DisplayName { get; set; }

    /// <summary>
    /// The form to be used to <see cref="IForm.Name"/>
    /// </summary>
    [NotNull]
    public string FormName { get; set; }

    /// <summary>
    /// Default value of the field.
    /// </summary>
    [CanBeNull]
    public string DefaultValue { get; set; }

    [NotNull]
    public FormConfigurationDictionary FormConfiguration { get; set; }
}