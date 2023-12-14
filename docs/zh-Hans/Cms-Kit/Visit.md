# 访问功能

Dignite CMS Kit提供了一个**访问**功能，可记录用户访问任何资源的历史。

## 启用访问功能

在开始使用之前，如果没有启用Dignite Cms Kit 全部功能，您需要单独启用**访问**功能：

在解决方案 `Domain.Shared` 项目中打开 `GlobalFeatureConfigurator` 类, 并将以下代码写入 `Configure` 方法中.

````csharp
GlobalFeatureManager.Instance.Modules.DigniteCmsKit(cmsKit =>
{
    cmsKit.Visits.Enable();
});
````

## 选项

**访问**功能提供了一种按实体类型分组访问的机制。例如，如果您希望记录用户**访问**过哪些商品，您需要定义一个名为`Product`的实体类型，然后在定义的实体类型下添加**访问**。

`CmsKitVisitOptions`可以在`Domain`中配置，在您的[模块的`ConfigureServices`方法](https://docs.abp.io/en/abp/latest/Module-Development-Basics)中配置。例如：

```csharp
Configure<CmsKitVisitOptions>(options =>
{
    options.EntityTypes.Add(new VisitEntityTypeDefinition("Product"));
});
```

## 域层

### 存储库

遵循[存储库最佳实践和约定](https://docs.abp.io/en/abp/latest/Best-Practices/Repositories)指南。

为此功能定义了以下自定义存储库：

- `IVisitRepository`

### 访问管理器

`VisitManager`用于执行`Visit`聚合根的一些操作。

## 应用层

### 应用服务

- `VisitPublicAppService`（实现`IVisitPublicAppService`）：实现访问功能的各种方法。

## HttpApi层

### Api接口

- `VisitPublicController`
  接口地址：api/cms-kit-public/visits
  实现了添加/删除访问的接口
