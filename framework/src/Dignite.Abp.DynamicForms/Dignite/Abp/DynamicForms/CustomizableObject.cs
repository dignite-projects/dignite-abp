﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Content;

namespace Dignite.Abp.DynamicForms;

[Serializable]
public abstract class CustomizableObject<TCustomizeFieldInfo> : IHasCustomFields, IValidatableObject
    where TCustomizeFieldInfo : class, ICustomizeFieldInfo
{
    protected CustomizableObject()
    {
        CustomFields = new();
        CustomizedFieldFiles = new();
    }

    /// <summary>
    ///
    /// </summary>
    [JsonInclude]
    public CustomFieldDictionary CustomFields { get; set; }

    /// <summary>
    /// All the uploaded file streams selected in the custom fields of file edit type
    /// </summary>
    public Dictionary<string, List<IRemoteStreamContent>> CustomizedFieldFiles { get; set; }

    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validationErrors = new List<ValidationResult>();
        var fieldDefinitions = GetFieldDefinitions(validationContext);
        var formSelector = validationContext.GetRequiredService<IFormSelector>();

        foreach (var field in CustomFields)
        {
            var fieldDefinition = fieldDefinitions.FirstOrDefault(fi => fi.Name.Equals(field.Key, StringComparison.OrdinalIgnoreCase));
            if (fieldDefinition == null)
                throw new AbpException($"No custom field named {field.Key} exists");

            var form = formSelector.Get(fieldDefinition.FormName);
            form.Validate(
                new FormValidateArgs(
                    fieldDefinition,
                    field.Value,
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
    public abstract IReadOnlyList<TCustomizeFieldInfo> GetFieldDefinitions(ValidationContext validationContext);
}