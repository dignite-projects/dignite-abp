using System;
using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms.Table;

[Serializable]
public class TableColumnCustomField : CustomizeFieldBase
{
    public TableColumnCustomField() : base() { }
    public TableColumnCustomField(
        [NotNull] string name,
        [NotNull] string displayName,
        [NotNull] string fieldName,
        [NotNull] string defaultValue,
        [NotNull] FormConfigurationDictionary configuration
        ) : base(name, displayName, fieldName, defaultValue, configuration) { }

}