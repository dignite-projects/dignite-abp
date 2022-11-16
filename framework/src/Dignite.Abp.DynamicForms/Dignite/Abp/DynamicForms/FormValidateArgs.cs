using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms;

public class FormValidateArgs
{
    public ICustomizeFieldInfo Field { get; }

    public object Value { get; }

    public List<ValidationResult> ValidationErrors { get; }

    public FormValidateArgs(
        [NotNull] ICustomizeFieldInfo field,
        object value,
        List<ValidationResult> validationErrors)
    {
        Field = field;
        Value = value;
        ValidationErrors = validationErrors;
    }
}