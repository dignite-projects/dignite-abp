using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text.Json;
using Volo.Abp;
using Volo.Abp.Reflection;

namespace Dignite.Abp.DynamicForms;

public static class FormConfigurationDictionaryExtensions
{
    public static bool HasConfiguration(this FormConfigurationDictionary source, string name)
    {
        return source.ContainsKey(name);
    }

    public static object? GetConfiguration(this FormConfigurationDictionary source, string name, object? defaultValue = null)
    {
        return source.GetOrDefault(name)
               ?? defaultValue;
    }

    public static TConfiguration GetConfiguration<TConfiguration>(this FormConfigurationDictionary source, string name, TConfiguration defaultValue = default)
    {
        var value = source.GetConfiguration(name);
        if (value == null)
        {
            return defaultValue;
        }

        try
        {
            var conversionType = typeof(TConfiguration);
            if (TypeHelper.IsNullable(conversionType))
            {
                conversionType = conversionType.GetFirstGenericArgumentIfNullable();
            }


            if (conversionType.IsEnum)
            {
                if (Enum.IsDefined(conversionType, (int)(long)value))
                {
                    return (TConfiguration)Enum.ToObject(conversionType, (int)(long)value);
                }
                else
                {
                    return defaultValue;
                }
            }

            return (TConfiguration)TypeDescriptor.GetConverter(conversionType).ConvertFromInvariantString(value.ToString()!)!;
        }
        catch(Exception exc)
        {
            if (value.GetType() == typeof(JsonElement))
            {
                return JsonSerializer.Deserialize<TConfiguration>(value.ToString(), new JsonSerializerOptions(JsonSerializerDefaults.Web));
            }
            else
            {
                return (TConfiguration)value;
            }
        }

    }

    public static void SetConfiguration<TConfiguration>(
        this FormConfigurationDictionary source,
        string name,
        TConfiguration value)
    {
        source[name] = value;
    }

    public static void RemoveConfiguration(this FormConfigurationDictionary source, string name)
    {
        source.Remove(name);
    }
}