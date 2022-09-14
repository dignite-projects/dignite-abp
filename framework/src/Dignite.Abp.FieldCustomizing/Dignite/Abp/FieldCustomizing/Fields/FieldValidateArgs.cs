using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing.Fields;

public class FieldValidateArgs
{
    public ICustomizeFieldDefinition FieldDefinition { get; }

    public object Value { get; }

    public List<ValidationResult> ValidationErrors { get; }

    public FieldValidateArgs(
        [NotNull] ICustomizeFieldDefinition fieldDefinition,
        object value,
        List<ValidationResult> validationErrors)
    {
        FieldDefinition = fieldDefinition;
        Value = value;
        ValidationErrors = validationErrors;
    }
}