# FieldCustomizing Module

`FieldCustomizing` 模块提供一系列方法，帮助您快速的构建自定义字段。

> 请参阅 [Dynamic Forms](Dynamic-Forms.md) ,了解动态表单和自定义字段，本文重点介绍自定义字段的数据库实现。

## 安装

在Domain项目中安装[Dignite.Abp.FieldCustomizing.Domain](https://www.nuget.org/packages/Dignite.Abp.FieldCustomizing.Domain)Nuget包，然后添加`[DependsOn(typeof(AbpFieldCustomizingDomainModule))]`模块依赖。

## CustomizeFieldDefinitionBase

`CustomizeFieldDefinitionBase`是自定义字段实体类的抽象类。

示例: 假设有一个直接继承`CustomizeFieldDefinitionBase`的 `BookFieldDefinition` 实体:

````csharp
public class BookFieldDefinition : CustomizeFieldDefinitionBase
{
    public string Group { get; set; }
}
````

关于`CustomizeFieldDefinitionBase`属性的介绍请参见[Dynamic Forms](Dynamic-Forms.md#ICustomizeFieldInfo)

## IHasCustomFields

`IHasCustomFields`是含有自定义字段值实体类的接口。

延续上面的代码，新建继承 `AggregateRoot<Guid>` 基类和`IHasCustomFields`接口的 `Book` 实体:

````csharp
public class Book : AuditedAggregateRoot<Guid>,IHasCustomFields
{
    public string Name { get; set; }
}
````

关于`IHasCustomFields`属性及方法的介绍请参见[Dynamic Forms](Dynamic-Forms.md#IHasCustomFields)

## Configure Ef Core Entity

`Dignite.Abp.FieldCustomizing.EntityFrameworkCore`有一些实用的扩展方法来配置从`ICustomizeFieldInfo`接口和`IHasCustomFields`接口继承的属性。

在EntityFrameworkCore项目中安装[Dignite.Abp.FieldCustomizing.EntityFrameworkCore](https://www.nuget.org/packages/Dignite.Abp.FieldCustomizing.EntityFrameworkCore)Nuget包，然后添加`[DependsOn(typeof(AbpFieldCustomizingEntityFrameworkCoreModule))]`模块依赖。

`ConfigureCustomizableFieldDefinitions()` 是配置字段定义实体基本属性和约定的方法。在你的 `DbContext` 重写 `OnModelCreating` 方法并且做以下配置:

````csharp
protected override void OnModelCreating(ModelBuilder builder)
{
    //Always call the base method
    base.OnModelCreating(builder);

    builder.Entity<BookFieldDefinition>(b =>
    {
        //Configure table
        b.ToTable("BookFieldDefinitions");

        b.ConfigureByConvention();
        b.ConfigureCustomizableFieldDefinitions();

        //Properties
        b.Property(q => q.Group).HasMaxLength(64);
    });
}
````

`ConfigureObjectCustomizedFields()` 是配置含有自定义字段值实体基本属性和约定的方法。在你的 `DbContext` 重写 `OnModelCreating` 方法并且做以下配置:

````csharp
protected override void OnModelCreating(ModelBuilder builder)
{
    //Always call the base method
    base.OnModelCreating(builder);    

    builder.Entity<Book>(b =>
    {
        //Configure table
        b.ToTable("Books");

        b.ConfigureByConvention();
        b.ConfigureObjectCustomizedFields();

        //Properties
        b.Property(q => q.Name).HasMaxLength(128);
    });
}
````

## See Also

- [Dignite.Cms Module](Cms.md)

基于 FieldCustomizing Module 开发的一套无头CMS。

