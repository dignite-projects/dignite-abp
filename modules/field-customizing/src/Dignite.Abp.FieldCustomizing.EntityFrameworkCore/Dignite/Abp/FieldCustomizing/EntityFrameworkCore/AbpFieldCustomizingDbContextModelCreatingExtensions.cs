using System;
using Dignite.Abp.DynamicForms;
using Dignite.Abp.FieldCustomizing.EntityFrameworkCore.ValueComparers;
using Dignite.Abp.FieldCustomizing.EntityFrameworkCore.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore;

public static class AbpFieldCustomizingDbContextModelCreatingExtensions
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="b"></param>
    public static void ConfigureCustomizableFieldDefinitions<T>(this EntityTypeBuilder<T> b)
        where T : class, ICustomizeField
    {
        b.As<EntityTypeBuilder>().TryConfigureCustomizableFieldDefinitions();
    }

    public static void TryConfigureCustomizableFieldDefinitions(this EntityTypeBuilder b)
    {
        if (!b.Metadata.ClrType.IsAssignableTo<ICustomizeField>())
        {
            return;
        }

        b.Property<string>(nameof(ICustomizeField.DisplayName)).IsRequired().HasMaxLength(64);
        b.Property<string>(nameof(ICustomizeField.Name)).IsRequired().HasMaxLength(64);
        b.Property<FormConfigurationDictionary>(nameof(ICustomizeField.FormConfiguration))
            .HasColumnName(nameof(ICustomizeField.FormConfiguration))
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
        where T : class, IHasCustomFields
    {
        b.As<EntityTypeBuilder>().TryConfigureObjectCustomizedFields();
    }

    public static void TryConfigureObjectCustomizedFields(this EntityTypeBuilder b)
    {
        if (!b.Metadata.ClrType.IsAssignableTo<IHasCustomFields>())
        {
            return;
        }

        b.Property<CustomFieldDictionary>(nameof(IHasCustomFields.CustomFields))
            .HasColumnName(nameof(IHasCustomFields.CustomFields))
            .HasConversion(new CustomizedFieldsValueConverter())
            .Metadata.SetValueComparer(new CustomizedFieldDictionaryValueComparer());
    }
}