using System;
using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms.Matrix;

[Serializable]
public class MatrixBlockFieldDefinition : CustomizeFieldBase
{
    public MatrixBlockFieldDefinition():base() { }
    public MatrixBlockFieldDefinition(
        [NotNull] string name,
        [NotNull] string displayName,
        [NotNull] string fieldName,
        [NotNull] string defaultValue,
        [NotNull] FormConfigurationDictionary configuration
        ):base(name,displayName,fieldName,defaultValue,configuration) { }

}