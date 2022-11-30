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
        where T : class, ICustomizeFieldInfo
    {
        b.As<EntityTypeBuilder>().TryConfigureCustomizableFieldDefinitions();
    }

    public static void TryConfigureCustomizableFieldDefinitions(this EntityTypeBuilder b)
    {
        if (!b.Metadata.ClrType.IsAssignableTo<ICustomizeFieldInfo>())
        {
            return;
        }

        b.Property<string>(nameof(ICustomizeFieldInfo.DisplayName)).IsRequired().HasMaxLength(CustomizeFieldInfoConsts.MaxDisplayNameLength);
        b.Property<string>(nameof(ICustomizeFieldInfo.Name)).IsRequired().HasMaxLength(CustomizeFieldInfoConsts.MaxNameLength);
        b.Property<string>(nameof(ICustomizeFieldInfo.FormName)).IsRequired().HasMaxLength(CustomizeFieldInfoConsts.MaxFormNameLength);
        b.Property<FormConfigurationDictionary>(nameof(ICustomizeFieldInfo.FormConfiguration))
            .HasColumnName(nameof(ICustomizeFieldInfo.FormConfiguration))
            .HasConversion(
                new FormConfigurationValueConverter()
                )
            .Metadata.SetValueComparer(new FormConfigurationDictionaryValueComparer());
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