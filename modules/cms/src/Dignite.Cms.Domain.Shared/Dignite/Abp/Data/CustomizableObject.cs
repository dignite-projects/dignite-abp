using Dignite.Abp.DynamicForms;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Volo.Abp.Data;

namespace Dignite.Abp.Data;

[Serializable]
public abstract class CustomizableObject : IHasCustomFields, IValidatableObject
{
    protected CustomizableObject()
    {
        ExtraProperties = new();
    }

    /// <summary>
    ///
    /// </summary>
    [JsonInclude]
    public ExtraPropertyDictionary ExtraProperties { get; set; }

    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validationErrors = new List<ValidationResult>();
        var customizeFields = GetCustomizeFields(validationContext);
        var formSelector = validationContext.GetRequiredService<IFormControlSelector>();

        foreach (var field in customizeFields)
        {
            var form = formSelector.Get(field.FormControlName);
            form.Validate(
                new FormControlValidateArgs(
                    field,
                    validationErrors
                )
            );
        }

        return validationErrors;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public abstract IReadOnlyList<FormField> GetCustomizeFields(ValidationContext validationContext);
}