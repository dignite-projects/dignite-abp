using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms;

public class FormControlValidateArgs
{
    public FormField Field { get; }

    public List<ValidationResult> ValidationErrors { get; }

    public FormControlValidateArgs(
        [NotNull] FormField field,
        List<ValidationResult> validationErrors)
    {
        Field = field;
        ValidationErrors = validationErrors;
    }
}