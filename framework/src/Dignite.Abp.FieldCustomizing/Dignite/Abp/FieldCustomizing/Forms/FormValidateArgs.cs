using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing.Forms;

public class FormValidateArgs
{
    public ICustomizeFieldDefinition FieldDefinition { get; }

    public object Value { get; }

    public List<ValidationResult> ValidationErrors { get; }

    public FormValidateArgs(
        [NotNull] ICustomizeFieldDefinition fieldDefinition,
        object value,
        List<ValidationResult> validationErrors)
    {
        FieldDefinition = fieldDefinition;
        Value = value;
        ValidationErrors = validationErrors;
    }
}