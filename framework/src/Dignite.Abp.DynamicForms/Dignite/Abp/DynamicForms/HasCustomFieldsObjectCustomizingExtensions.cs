using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms;

public static class HasCustomFieldsObjectCustomizingExtensions
{

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
        where TSource : IHasCustomFields
        where TDestination : IHasCustomFields
    {
        CustomizableObjectMapper.MapCustomizeFieldsTo(
            typeof(TSource),
            typeof(TDestination),
            source.CustomFields,
            destination.CustomFields,
            fields,
            ignoredFields
            );
    }
}