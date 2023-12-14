# 喜欢功能

Dignite CMS Kit提供了一个**喜欢**功能，可向任何资源添加喜欢/收藏功能，例如用户喜欢一件商品、收藏一篇博客文章等。以下是示例页面上的外观：

![喜欢](../images/cmskit-module-favourites.png)

## 启用喜欢功能

在开始使用之前，如果没有启用Dignite Cms Kit 全部功能，您需要单独启用**喜欢**功能：

在解决方案 `Domain.Shared` 项目中打开 `GlobalFeatureConfigurator` 类, 并将以下代码写入 `Configure` 方法中.

````csharp
GlobalFeatureManager.Instance.Modules.DigniteCmsKit(cmsKit =>
{
    cmsKit.Favourites.Enable();
});
````

## 选项

**喜欢**功能提供了一种按实体类型分组喜欢的机制。例如，如果您希望将**喜欢**功能用于商品，您需要定义一个名为`Product`的实体类型，然后在定义的实体类型下添加**喜欢**。

`CmsKitFavouriteOptions`可以在`Domain`中配置，在您的[模块的`ConfigureServices`方法](https://docs.abp.io/en/abp/latest/Module-Development-Basics)中配置。例如：

```csharp
Configure<CmsKitFavouriteOptions>(options =>
{
    options.EntityTypes.Add(new FavouriteEntityTypeDefinition("Product"));
});
```

## 域层

### 存储库

遵循[存储库最佳实践和约定](https://docs.abp.io/en/abp/latest/Best-Practices/Repositories)指南。

为此功能定义了以下自定义存储库：

- `IFavouriteRepository`

### 喜欢管理器

`FavouriteManager`用于执行`Favourite`聚合根的一些操作。

## 应用层

### 应用服务

- `FavouritePublicAppService`（实现`IFavouritePublicAppService`）：实现喜欢功能的各种方法。

## HttpApi层

### Api接口

- `FavouritePublicController`
  接口地址：api/cms-kit-public/favourites
  实现了添加/删除喜欢的接口
