using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;

namespace Dignite.Abp.FieldCustomizing
{
    public static class CustomizableObjectMapper
    {

        /// <summary>
        /// Copies customized fields from the <paramref name="sourceDictionary"/> object
        /// to the <paramref name="destinationDictionary"/> object.
        /// </summary>
        /// <typeparam name="TSource">Source class type (for definition check)</typeparam>
        /// <typeparam name="TDestination">Destination class type (for definition check)</typeparam>
        /// <param name="sourceDictionary">The source dictionary object</param>
        /// <param name="destinationDictionary">The destination dictionary object</param>
        /// <param name="fields">Used to map fields;When the value is null, all fields are mapped</param>
        /// <param name="ignoredFields">Used to ignore some fields</param>
        public static void MapCustomizeFieldsTo<TSource, TDestination>(
            [NotNull] Dictionary<string, object> sourceDictionary,
            [NotNull] Dictionary<string, object> destinationDictionary,
            string[] fields = null,
            string[] ignoredFields = null)
            where TSource : IHasCustomizableFields
            where TDestination : IHasCustomizableFields
        {
            MapCustomizeFieldsTo(
                typeof(TSource),
                typeof(TDestination),
                sourceDictionary,
                destinationDictionary,
                fields,
                ignoredFields
            );
        }


        /// <summary>
        /// Copies customized fields from the <paramref name="sourceDictionary"/> object
        /// to the <paramref name="destinationDictionary"/> object.
        /// </summary>
        /// <param name="sourceType">Source type (for definition check)</param>
        /// <param name="destinationType">Destination class type (for definition check)</param>
        /// <param name="sourceDictionary">The source dictionary object</param>
        /// <param name="destinationDictionary">The destination dictionary object</param>
        /// <param name="fields">Used to map fields;When the value is null, all fields are mapped</param>
        /// <param name="ignoredFields">Used to ignore some fields</param>
        public static void MapCustomizeFieldsTo(
            [NotNull] Type sourceType,
            [NotNull] Type destinationType,
            [NotNull] Dictionary<string, object> sourceDictionary,
            [NotNull] Dictionary<string, object> destinationDictionary,
            string[] fields = null,
            string[] ignoredFields = null)
        {
            Check.AssignableTo<IHasCustomizableFields>(sourceType, nameof(sourceType));
            Check.AssignableTo<IHasCustomizableFields>(destinationType, nameof(destinationType));
            Check.NotNull(sourceDictionary, nameof(sourceDictionary));
            Check.NotNull(destinationDictionary, nameof(destinationDictionary));


            foreach (var keyValue in sourceDictionary)
            {
                if (ignoredFields != null &&
                    ignoredFields.Contains(keyValue.Key))
                {
                    continue;
                }

                if (fields == null)
                {
                    destinationDictionary[keyValue.Key] = keyValue.Value;
                }
                else if (fields != null &&
                    fields.Contains(keyValue.Key))
                {
                    destinationDictionary[keyValue.Key] = keyValue.Value;
                }
            }
        }
    }
}
