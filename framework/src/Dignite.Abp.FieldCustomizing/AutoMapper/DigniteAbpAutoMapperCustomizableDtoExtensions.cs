using Dignite.Abp.FieldCustomizing;
using System.Collections.Generic;

namespace AutoMapper
{
    public static class DigniteAbpAutoMapperCustomizableDtoExtensions
    {
        public static IMappingExpression<TSource, TDestination> MapCustomizeFields<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression,
            string[] fields = null,
            string[] ignoredFields = null,
            bool mapToRegularProperties = false)
            where TDestination : IHasCustomizableFields
            where TSource : IHasCustomizableFields
        {
            return mappingExpression
                .ForMember(
                    x => x.CustomizedFields,
                    y => y.MapFrom(
                        (source, destination, extraProps) =>
                        {
                            var result = extraProps.IsNullOrEmpty()
                                ? new Dictionary<string, object>()
                                : new Dictionary<string, object>(extraProps);

                            CustomizableObjectMapper
                                .MapCustomizeFieldsTo<TSource, TDestination>(
                                    source.CustomizedFields,
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
}