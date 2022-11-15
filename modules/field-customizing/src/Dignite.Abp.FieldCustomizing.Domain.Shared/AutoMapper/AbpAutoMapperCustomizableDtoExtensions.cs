using System.Collections.Generic;
using Dignite.Abp.DynamicForms;
using Dignite.Abp.FieldCustomizing;

namespace AutoMapper;

public static class AbpAutoMapperCustomizableDtoExtensions
{
    public static IMappingExpression<TSource, TDestination> MapCustomizeFields<TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> mappingExpression,
        string[] fields = null,
        string[] ignoredFields = null,
        bool mapToRegularProperties = false)
        where TDestination : IHasCustomFields
        where TSource : IHasCustomFields
    {
        return mappingExpression
            .ForMember(
                x => x.CustomFields,
                y => y.MapFrom(
                    (source, destination, extraProps) =>
                    {
                        var result = extraProps.IsNullOrEmpty()
                            ? new Dictionary<string, object>()
                            : new Dictionary<string, object>(extraProps);

                        CustomizableObjectMapper
                            .MapCustomizeFieldsTo<TSource, TDestination>(
                                source.CustomFields,
                                result,
                                fields,
                                ignoredFields
                            );

                        return result;
                    })
            )
            .AfterMap((source, destination, context) =>
            {
                if (mapToRegularProperties)
                {
                    destination.SetCustomizeFieldsToRegularProperties();
                }
            });
    }
}