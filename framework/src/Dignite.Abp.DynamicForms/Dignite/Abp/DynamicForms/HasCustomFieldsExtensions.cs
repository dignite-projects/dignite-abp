using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using JetBrains.Annotations;
using Volo.Abp.Reflection;

namespace Dignite.Abp.DynamicForms;

public static class HasCustomFieldsExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool HasField([NotNull] this IHasCustomFields source, [NotNull] string name)
    {
        return source.CustomFields.ContainsKey(name);
    }

    public static object GetField([NotNull] this IHasCustomFields source, [NotNull] string name, object defaultValue = null)
    {
        return source.CustomFields?.GetOrDefault(name)
               ?? defaultValue;
    }

    public static TField GetField<TField>([NotNull] this IHasCustomFields source, [NotNull] string name, TField defaultValue = default)
    {
        var value = source.GetField(name);
        if (value == null)
        {
            return defaultValue;
        }

        if (TypeHelper.IsPrimitiveExtended(typeof(TField), includeEnums: true))
        {
            var conversionType = typeof(TField);
            if (TypeHelper.IsNullable(conversionType))
            {
                conversionType = conversionType.GetFirstGenericArgumentIfNullable();
            }

            if (conversionType == typeof(Guid)|| conversionType == typeof(String))
            {
                return (TField)TypeDescriptor.GetConverter(conversionType).ConvertFromInvariantString(value.ToString());
            }

            return (TField)Convert.ChangeType(value, conversionType, CultureInfo.InvariantCulture);
        }
        else
        {
            return JsonSerializer.Deserialize<TField>(value.ToString(), new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }
    }

    public static TSource SetField<TSource>(
        this TSource source,
        string name,
        object value)
        where TSource : IHasCustomFields
    {
        source.CustomFields[name] = value;

        return source;
    }

    public static TSource RemoveField<TSource>(this TSource source, string name)
        where TSource : IHasCustomFields
    {
        source.CustomFields.Remove(name);
        return source;
    }

    public static TSource SetDefaultsForCustomizeFields<TSource>(this TSource source, IReadOnlyList<ICustomizeFieldInfo> customizeFields)
        where TSource : IHasCustomFields
    {
        foreach (var field in customizeFields)
        {
            if (source.HasField(field.Name))
            {
                continue;
            }

            source.CustomFields[field.Name] = field.DefaultValue;
        }

        return source;
    }

    public static void SetCustomizeFieldsToRegularProperties([NotNull] this IHasCustomFields source)
    {
        var properties = source.GetType().GetProperties()
            .Where(x => source.CustomFields.ContainsKey(x.Name)
                        && x.GetSetMethod(true) != null)
            .ToList();

        foreach (var property in properties)
        {
            property.SetValue(source, source.CustomFields[property.Name]);
            source.RemoveField(property.Name);
        }
    }
}