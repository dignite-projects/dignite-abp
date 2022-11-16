using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using Dignite.Abp.DynamicForms;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volo.Abp.Json.SystemTextJson.JsonConverters;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore.ValueConverters;

public class FormConfigurationValueConverter : ValueConverter<FormConfigurationDictionary, string>
{
    public FormConfigurationValueConverter()
        : base(
            d => SerializeObject(d),
            s => DeserializeObject(s))
    {
    }

    private static string SerializeObject(FormConfigurationDictionary extraProperties)
    {
        var serializeOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        return JsonSerializer.Serialize(extraProperties, serializeOptions);
    }

    private static FormConfigurationDictionary DeserializeObject(string extraPropertiesAsJson)
    {
        if (extraPropertiesAsJson.IsNullOrEmpty() || extraPropertiesAsJson == "{}")
        {
            return new FormConfigurationDictionary();
        }

        var deserializeOptions = new JsonSerializerOptions();
        deserializeOptions.Converters.Add(new ObjectToInferredTypesConverter());

        var dictionary = JsonSerializer.Deserialize<FormConfigurationDictionary>(extraPropertiesAsJson, deserializeOptions) ??
                         new FormConfigurationDictionary();

        return dictionary;
    }
}