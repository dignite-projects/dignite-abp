using Dignite.Abp.FieldCustomizing.EntityFrameworkCore.ValueComparers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Dignite.Abp.FieldCustomizing.EntityFrameworkCore.ValueConverters;
using Dignite.Abp.FieldCustomizing.Fields;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore.Modeling
{
    public static class FieldsEntityTypeBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="b"></param>
        public static void ConfigureCustomizableFieldDefinitions<T>(this EntityTypeBuilder<T> b)
            where T : class, ICustomizeFieldDefinition
        {
            b.As<EntityTypeBuilder>().TryConfigureCustomizableFieldDefinitions();
        }

        public static void TryConfigureCustomizableFieldDefinitions(this EntityTypeBuilder b)
        {
            if (!b.Metadata.ClrType.IsAssignableTo<ICustomizeFieldDefinition>())
            {
                return;
            }

            b.Property<string>(nameof(ICustomizeFieldDefinition.DisplayName)).IsRequired().HasMaxLength(64);
            b.Property<string>(nameof(ICustomizeFieldDefinition.Name)).IsRequired().HasMaxLength(64);
            b.Property<FieldConfigurationDictionary>(nameof(ICustomizeFieldDefinition.Configuration))
                .HasColumnName(nameof(ICustomizeFieldDefinition.Configuration))
                .HasConversion(
                    new CustomizedFieldConfigurationValueConverter()
                    )
                .Metadata.SetValueComparer(new CustomizedFieldConfigurationDictionaryValueComparer());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="b"></param>
        public static void ConfigureObjectCustomizedFields<T>(this EntityTypeBuilder<T> b)
            where T : class, IHasCustomizableFields
        {
            b.As<EntityTypeBuilder>().TryConfigureObjectCustomizedFields();
        }

        public static void TryConfigureObjectCustomizedFields(this EntityTypeBuilder b)
        {
            if (!b.Metadata.ClrType.IsAssignableTo<IHasCustomizableFields>())
            {
                return;
            }

            b.Property<CustomizeFieldDictionary>(nameof(IHasCustomizableFields.CustomizedFields))
                .HasColumnName(nameof(IHasCustomizableFields.CustomizedFields))
                .HasConversion(new CustomizedFieldsValueConverter())
                .Metadata.SetValueComparer(new CustomizedFieldDictionaryValueComparer());
        }
    }
}
