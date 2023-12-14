# Favorite Feature

Dignite CMS Kit provides a **Favorite** feature that allows adding favorite/collection functionality to any resource. For example, users can mark a product as a favorite or collect a blog post. Below is the appearance on a sample page:

![Favorite](../images/cmskit-module-favourites.png)

## Enable Favorite Feature

Before getting started, if you haven't enabled all features of Dignite Cms Kit, you need to enable the **Favorite** feature separately:

Open the `GlobalFeatureConfigurator` class in the solution's `Domain.Shared` project and add the following code to the `Configure` method.

````csharp
GlobalFeatureManager.Instance.Modules.DigniteCmsKit(cmsKit =>
{
    cmsKit.Favourites.Enable();
});
````

## Options

The **Favorite** feature provides a mechanism to group favorites by entity type. For example, if you want to use the **Favorite** feature for products, you need to define an entity type named `Product` and then add favorites under the defined entity type.

`CmsKitFavouriteOptions` can be configured in the `Domain` by configuring it in the `ConfigureServices` method of your [module](https://docs.abp.io/en/abp/latest/Module-Development-Basics). For example:

```csharp
Configure<CmsKitFavouriteOptions>(options =>
{
    options.EntityTypes.Add(new FavouriteEntityTypeDefinition("Product"));
});
```

## Domain Layer

### Repositories

Follow the [best practices and conventions for repositories](https://docs.abp.io/en/abp/latest/Best-Practices/Repositories) guide.

Custom repositories defined for this feature include:

- `IFavouriteRepository`

### Favorite Manager

The `FavouriteManager` is used to perform some operations on the `Favourite` aggregate root.

## Application Layer

### Application Services

- `FavouritePublicAppService` (implements `IFavouritePublicAppService`): Implements various methods for the favorite feature.

## HttpApi Layer

### API Interface

- `FavouritePublicController`
  API endpoint: api/cms-kit-public/favourites
  Implements interfaces for adding/deleting favorites.
  