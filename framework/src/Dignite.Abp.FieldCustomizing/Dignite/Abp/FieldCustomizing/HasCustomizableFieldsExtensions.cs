using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Reflection;

namespace Dignite.Abp.FieldCustomizing
{
    public static class HasCustomizableFieldsExtensions
    {
        public static bool HasField([NotNull] this IHasCustomizableFields source, [NotNull] string name)
        {
            return source.CustomizedFields.ContainsKey(name);
        }

        public static object GetField([NotNull] this IHasCustomizableFields source, [NotNull] string name, object defaultValue = null)
        {
            return source.CustomizedFields?.GetOrDefault(name)
                   ?? defaultValue;
        }

        public static TField GetField<TField>([NotNull] this IHasCustomizableFields source, [NotNull] string name, TField defaultValue = default)
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

                if (conversionType == typeof(Guid))
                {
                    return (TField)TypeDescriptor.GetConverter(conversionType).ConvertFromInvariantString(value.ToString());
                }

                return (TField)Convert.ChangeType(value, conversionType, CultureInfo.InvariantCulture);
            }

            throw new AbpException("GetField<TField> does not support non-primitive types. Use non-generic GetField method and handle type casting manually.");
        }

        public static TSource SetField<TSource>(
            this TSource source,
            string name,
            object value)
            where TSource : IHasCustomizableFields
        {
            source.CustomizedFields[name] = value;

            return source;
        }

        public static TSource RemoveField<TSource>(this TSource source, string name)
            where TSource : IHasCustomizableFields
        {
            source.CustomizedFields.Remove(name);
            return source;
        }

        public static TSource SetDefaultsForCustomizeFields<TSource>(this TSource source, IReadOnlyList<BasicCustomizeFieldDefinition> fieldDefinitions)
            where TSource : IHasCustomizableFields
        {
            foreach (var fieldDefinition in fieldDefinitions)
            {
                if (source.HasField(fieldDefinition.Name))
                {
                    continue;
                }

                source.CustomizedFields[fieldDefinition.Name] = fieldDefinition.DefaultValue;
            }

            return source;
        }

        public static void SetCustomizeFieldsToRegularProperties([NotNull] this IHasCustomizableFields source)
        {
            var properties = source.GetType().GetProperties()
                .Where(x => source.CustomizedFields.ContainsKey(x.Name)
                            && x.GetSetMethod(true) != null)
                .ToList();

            foreach (var property in properties)
            {
                property.SetValue(source, source.CustomizedFields[property.Name]);
                source.RemoveField(property.Name);
            }
        }


        /// <summary>
        /// Copies customized fields from the <paramref name="source"/> object
        /// to the <paramref name="destination"/> object.
        /// </summary>
        /// <typeparam name="TSource">Source class type</typeparam>
        /// <typeparam name="TDestination">Destination class type</typeparam>
        /// <param name="source">The source object</param>
        /// <param name="destination">The destination object</param>
        /// <param name="fields">Used to map fields;When the value is null, all fields are mapped</param>
        /// <param name="ignoredFields">Used to ignore some fields</param>
        public static void MapCustomizeFieldsTo<TSource, TDestination>(
            [NotNull] this TSource source,
            [NotNull] TDestination destination,
            string[] fields = null,
            string[] ignoredFields = null)
            where TSource : IHasCustomizableFields
            where TDestination : IHasCustomizableFields
        {
            CustomizableObjectMapper.MapCustomizeFieldsTo(
                typeof(TSource),
                typeof(TDestination),
                source.CustomizedFields,
                destination.CustomizedFields,
                fields,
                ignoredFields
                );
        }
    }
}
