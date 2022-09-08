using Dignite.Abp.FieldCustomizing.Fields;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Volo.Abp.Json.SystemTextJson.JsonConverters;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore.ValueConverters
{
    public  class CustomizedFieldConfigurationValueConverter : ValueConverter<FieldConfigurationDictionary, string>
    {
        public CustomizedFieldConfigurationValueConverter()
            : base(
                d => SerializeObject(d),
                s => DeserializeObject(s))
        {

        }

        private static string SerializeObject(FieldConfigurationDictionary extraProperties)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            return JsonSerializer.Serialize(extraProperties, serializeOptions);
        }

        private static FieldConfigurationDictionary DeserializeObject(string extraPropertiesAsJson)
        {
            if (extraPropertiesAsJson.IsNullOrEmpty() || extraPropertiesAsJson == "{}")
            {
                return new FieldConfigurationDictionary();
            }

            var deserializeOptions = new JsonSerializerOptions();
            deserializeOptions.Converters.Add(new ObjectToInferredTypesConverter());

            var dictionary = JsonSerializer.Deserialize<FieldConfigurationDictionary>(extraPropertiesAsJson, deserializeOptions) ??
                             new FieldConfigurationDictionary();


            return dictionary;
        }

    }
}
