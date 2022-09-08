using Dignite.Abp.FieldCustomizing.Fields;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using Volo.Abp.Content;

namespace Dignite.Abp.FieldCustomizing;

[Serializable]
public abstract class CustomizableObject<T> : IHasCustomizableFields, IValidatableObject
    where T : class, ICustomizeFieldDefinition
{
    protected CustomizableObject()
    {
        CustomizedFields = new();
        CustomizedFieldFiles = new();
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public CustomizeFieldDictionary CustomizedFields { get; set; }

    /// <summary>
    /// Select the uploaded file stream content
    /// </summary>
    public Dictionary<string, List<IRemoteStreamContent>> CustomizedFieldFiles { get; set; }

    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validationErrors = new List<ValidationResult>();
        var fieldDefinitions = GetFieldDefinitions(validationContext);
        var fieldProviderSelector = validationContext.GetRequiredService<IFieldProviderSelector>();

        foreach (var customField in CustomizedFields)
        {
            var fieldDefinition = fieldDefinitions.FirstOrDefault(fi => fi.Name == customField.Key);
            if (fieldDefinition == null)
                continue;
            var fieldProvider = fieldProviderSelector.Get(fieldDefinition.FieldProviderName);
            fieldProvider.Validate(
                new FieldValidateArgs(
                    fieldDefinition,
                    customField.Value,
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
    public abstract IReadOnlyList<T> GetFieldDefinitions(ValidationContext validationContext);
}
