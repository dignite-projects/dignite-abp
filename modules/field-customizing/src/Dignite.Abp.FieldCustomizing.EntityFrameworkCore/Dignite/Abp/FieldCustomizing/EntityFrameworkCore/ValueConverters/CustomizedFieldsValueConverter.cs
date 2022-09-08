using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volo.Abp.Json.SystemTextJson.JsonConverters;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore.ValueConverters
{
    public class CustomizedFieldsValueConverter : ValueConverter<CustomizeFieldDictionary, string>
    {
        public CustomizedFieldsValueConverter()
            : base(
                d => SerializeObject(d),
                s => DeserializeObject(s))
        {

        }

        private static string SerializeObject(CustomizeFieldDictionary extraFields)
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

        private static CustomizeFieldDictionary DeserializeObject(string extraFieldsAsJson)
        {
            if (extraFieldsAsJson.IsNullOrEmpty() || extraFieldsAsJson == "{}")
            {
                return new CustomizeFieldDictionary();
            }

            var deserializeOptions = new JsonSerializerOptions();
            deserializeOptions.Converters.Add(new ObjectToInferredTypesConverter());
            var dictionary = JsonSerializer.Deserialize<CustomizeFieldDictionary>(extraFieldsAsJson, deserializeOptions) ??
                             new CustomizeFieldDictionary();

            return dictionary;
        }

    }
}
