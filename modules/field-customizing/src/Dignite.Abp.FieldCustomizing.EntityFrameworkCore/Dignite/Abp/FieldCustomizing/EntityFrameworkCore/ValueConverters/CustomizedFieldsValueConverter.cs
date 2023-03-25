using System;
using System.Collections.Generic;
using System.Linq;
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

    private static string SerializeObject(CustomFieldDictionary customFields)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        /*
        var serializeValue = JsonSerializer.Serialize(
            customFields.Select(f => new KeyValuePair<string, object>(f.Key, f.Value)),
            options);
        */

        var serializeValue = JsonSerializer.Serialize(customFields, options);
        return serializeValue;
    }

    private static CustomFieldDictionary DeserializeObject(string customFieldsAsJson)
    {
        if (customFieldsAsJson.IsNullOrEmpty() || customFieldsAsJson == "{}" || customFieldsAsJson == "[]")
        {
            return new CustomFieldDictionary();
        }

        var deserializeOptions = new JsonSerializerOptions();
        deserializeOptions.Converters.Add(new ObjectToInferredTypesConverter());

        /*
        var dictionary = JsonSerializer.Deserialize<List<KeyValuePair<string, object>>>(
            customFieldsAsJson,
            deserializeOptions)
            .ToDictionary(kv => kv.Key, kv => kv.Value);
        */

        var dictionary = JsonSerializer.Deserialize<CustomFieldDictionary>(customFieldsAsJson, deserializeOptions) ??
                         new CustomFieldDictionary();

        return dictionary;
    }
}