using System;
using System.Text.Json;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Reflection;

namespace Dignite.Abp.Data;

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
        return source.HasProperty(name);
    }

    public static object GetField([NotNull] this IHasCustomFields source, [NotNull] string name, object defaultValue = null)
    {
        return source.GetProperty(name, defaultValue);
    }

    public static TField GetField<TField>([NotNull] this IHasCustomFields source, [NotNull] string name, TField defaultValue = default)
    {
        try
        {
            return source.GetProperty<TField>(name, defaultValue);
        }
        catch (Exception exc)
        {
            var value = source.GetProperty(name);
            if (value == null)
            {
                return defaultValue;
            }

            if (TypeHelper.IsPrimitiveExtended(typeof(TField)))
                return ((JsonElement)value).Deserialize<TField>(JsonSerializerOptions.Default);
            else
                return JsonSerializer.Deserialize<TField>(value.ToString(), new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }
    }

    public static TSource SetField<TSource>(
        this TSource source,
        string name,
        object value)
        where TSource : IHasCustomFields
    {
        return source.SetProperty(name, value, true);
    }

    public static TSource RemoveField<TSource>(this TSource source, string name)
        where TSource : IHasCustomFields
    {
        return source.RemoveProperty(name);
    }


    public static void SetCustomizeFieldsToRegularProperties([NotNull] this IHasCustomFields source)
    {
        source.SetExtraPropertiesToRegularProperties();
    }
}