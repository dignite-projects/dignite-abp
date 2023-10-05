# Custom Field Basics Module

Dynamic forms require database persistence techniques to manage custom fields and custom field values for business objects. This module provides a set of methods to help you quickly build business objects that contain custom fields.

## Installation

- Install the `Dignite.Abp.FieldCustomizing.Domain.Shared` NuGet package in your `Domain.Shared` project.

- Add `DigniteAbpFieldCustomizingDomainSharedModule` to the `[DependsOn(...)]` attribute list of your `Domain.Shared` [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

- Install the `Dignite.Abp.FieldCustomizing.Domain` NuGet package in your `Domain` project.

- Add `AbpFieldCustomizingDomainModule` to the `[DependsOn(...)]` attribute list of your `Domain` [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

- Install the `Dignite.Abp.FieldCustomizing.EntityFrameworkCore` NuGet package in your `EntityFrameworkCore` project.

- Add `DigniteAbpFieldCustomizingEntityFrameworkCoreModule` to the `[DependsOn(...)]` attribute list of your `EntityFrameworkCore` [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

## Creating Field Entity Classes

`CustomizeFieldDefinitionBase` is an abstract class used to define field entity classes. It implements the [ICustomizeFieldInfo](Dynamic-Forms.md#defining-field-information) interface.

> It is also possible to create field entity classes by inheriting from `ICustomizeFieldInfo`, but inheriting from `CustomizeFieldDefinitionBase` can reduce code.

Example:

```csharp
public class ProductFieldDefinition : CustomizeFieldDefinitionBase
{    
    /// <summary>
    /// Field Group
    /// </summary>
    public string Group { get; set; }
}
```

## Creating Business Object Entity Classes

[IHasCustomFields](Dynamic-Forms.md#business-objects-with-custom-fields) is an interface used to manage custom field values for business object entity classes.

Example:

```csharp
public class Product : AuditedAggregateRoot<Guid>, IHasCustomFields
{    
    /// <summary>
    /// Product Category Id
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Product Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Product Sku Info
    /// </summary>
    public CustomFieldDictionary CustomFields { get; set; }
}
```

## Configuring Ef Core Entities

`Dignite.Abp.FieldCustomizing.EntityFrameworkCore` provides some useful extension methods to configure implementations of the `ICustomizeFieldInfo` and `IHasCustomFields` interfaces.

- `ConfigureCustomizableFieldDefinitions()` Method

    This method is used to configure field definition entity properties and conventions. Configure it in your `DbContext` `OnModelCreating` method as follows:

    ```csharp
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Always call the base method
        base.OnModelCreating(builder);

        builder.Entity<ProductFieldDefinition>(b =>
        {
            // Configure table
            b.ToTable("ProductFieldDefinitions");

            b.ConfigureByConvention();

            // Configure Field
            b.ConfigureCustomizableFieldDefinitions();

            // Properties
            b.Property(q => q.Group).HasMaxLength(64);
        });
    }
    ```

- `ConfigureObjectCustomizedFields()` Method

    This method is used to configure entity properties and conventions for business objects containing custom field values. Configure it in your `DbContext` `OnModelCreating` method as follows:

    ```csharp
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Always call the base method
        base.OnModelCreating(builder);    

        builder.Entity<Product>(b =>
        {
            // Configure table
            b.ToTable("Products");

            b.ConfigureByConvention();

            // Configure Object Customized Fields
            b.ConfigureObjectCustomizedFields();

            // Properties
            b.Property(q => q.Name).HasMaxLength(128);
        });
    }
    ```

## Recommended Reading

- [Dignite.Cms](https://dignite.com/dignite-cms)

    A content management system developed based on the [Custom Field Basics Module](Field-Customizing.md) with flexible form definitions.
