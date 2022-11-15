using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using Dignite.Abp.DynamicForms;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volo.Abp.Json.SystemTextJson.JsonConverters;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore.ValueConverters;

public class CustomizedFieldsValueConverter : ValueConverter<CustomFieldDictionary, string>
{
    public CustomizedFieldsValueConverter()
        : base(
            d => SerializeObject(d),
            s => DeserializeObject(s))
    {
    }

    private static string SerializeObject(CustomFieldDictionary extraFields)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        var copyDictionary = new Dictionary<string, object>(extraFields);

        var serializeValue = JsonSerializer.Serialize(copyDictionary, options);
        return serializeValue;
    }

    private static CustomFieldDictionary DeserializeObject(string extraFieldsAsJson)
    {
        if (extraFieldsAsJson.IsNullOrEmpty() || extraFieldsAsJson == "{}")
        {
            return new CustomFieldDictionary();
        }

        var deserializeOptions = new JsonSerializerOptions();
        deserializeOptions.Converters.Add(new ObjectToInferredTypesConverter());
        var dictionary = JsonSerializer.Deserialize<CustomFieldDictionary>(extraFieldsAsJson, deserializeOptions) ??
                         new CustomFieldDictionary();

        return dictionary;
    }
}