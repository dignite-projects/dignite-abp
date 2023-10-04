# 自定义字段基础模块

动态表单需要配合数据库持久化技术，管理自定义字段以及业务对象的自定义字段值。本模块提供一系列方法，帮助您快速的构建含有自定义字段的业务对象。

## 安装

- 将 `Dignite.Abp.FieldCustomizing.Domain.Shared` Nuget 包安装到 `Domain.Shared` 项目中

- 添加 `DigniteAbpFieldCustomizingDomainSharedModule` 到 `Domain.Shared` [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

- 将 `Dignite.Abp.FieldCustomizing.Domain` Nuget 包安装到 `Domain` 项目中

- 添加 `AbpFieldCustomizingDomainModule` 到 `Domain` [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

- 将 `Dignite.Abp.FieldCustomizing.EntityFrameworkCore` Nuget 包安装到 `EntityFrameworkCore` 项目中

- 添加 `DigniteAbpFieldCustomizingEntityFrameworkCoreModule` 到 `EntityFrameworkCore` [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

## 创建字段实体类

`CustomizeFieldDefinitionBase`是定义字段实体类的抽象类，它是 [ICustomizeFieldInfo](Dynamic-Forms.md#定义字段信息) 接口的实现。

> 继承 `ICustomizeFieldInfo` 创建字段实体类也是可以的，但继承自`CustomizeFieldDefinitionBase`创建字段实体类可以减少代码。

示例:

````csharp
public class ProductFieldDefinition : CustomizeFieldDefinitionBase
{    
    /// <summary>
    /// Field Group
    /// </summary>
    public string Group { get; set; }
}
````

## 创建业务对象实体类

[IHasCustomFields](Dynamic-Forms.md#含有自定义字段的业务对象)是含有自定义字段值实体类的接口，用于管理自定义字段的值。

示例：

````csharp
public class Product : AuditedAggregateRoot<Guid>,IHasCustomFields
{    
    /// <summary>
    /// Product Cateogry Id
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
````

## 配置 Ef Core 实体

`Dignite.Abp.FieldCustomizing.EntityFrameworkCore`有一些实用的扩展方法配置`ICustomizeFieldInfo`接口和`IHasCustomFields`接口的实现类。

- `ConfigureCustomizableFieldDefinitions()` 方法

    是配置字段定义实体属性和约定的方法，在你的 `DbContext` `OnModelCreating` 方法中做以下配置:

    ````csharp
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Always call the base method
        base.OnModelCreating(builder);

        builder.Entity<ProductFieldDefinition>(b =>
        {
            //Configure table
            b.ToTable("ProductFieldDefinitions");

            b.ConfigureByConvention();

            //Configure Field
            b.ConfigureCustomizableFieldDefinitions();

            //Properties
            b.Property(q => q.Group).HasMaxLength(64);
        });
    }
    ````

- `ConfigureObjectCustomizedFields()` 方法

    是配置含有自定义字段值实体属性和约定的方法，在你的 `DbContext` `OnModelCreating` 方法中做以下配置:

    ````csharp
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Always call the base method
        base.OnModelCreating(builder);    

        builder.Entity<Product>(b =>
        {
            //Configure table
            b.ToTable("Products");

            b.ConfigureByConvention();

            //Configure Object Customized Fields
            b.ConfigureObjectCustomizedFields();

            //Properties
            b.Property(q => q.Name).HasMaxLength(128);
        });
    }
    ````

## 推荐阅读

- [Dignite.Cms](https://dignite.com/dignite-cms)

    基于[自定义字段基础模块](Field-Customizing.md)开发的一套具有灵活表单定义的内容管理系统。
