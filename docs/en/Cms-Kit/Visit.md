# Visit Feature

Dignite CMS Kit provides a **Visit** feature that records the history of users accessing any resource.

## Enable Visit Feature

Before getting started, if you haven't enabled all features of Dignite Cms Kit, you need to enable the **Visit** feature separately:

Open the `GlobalFeatureConfigurator` class in the solution's `Domain.Shared` project and add the following code to the `Configure` method.

````csharp
GlobalFeatureManager.Instance.Modules.DigniteCmsKit(cmsKit =>
{
    cmsKit.Visits.Enable();
});
````

## Options

The **Visit** feature provides a mechanism to group visits by entity type. For example, if you want to record which products users have **visited**, you need to define an entity type named `Product` and then add visits under the defined entity type.

`CmsKitVisitOptions` can be configured in the `Domain` by configuring it in the `ConfigureServices` method of your [module](https://docs.abp.io/en/abp/latest/Module-Development-Basics). For example:

```csharp
Configure<CmsKitVisitOptions>(options =>
{
    options.EntityTypes.Add(new VisitEntityTypeDefinition("Product"));
});
```

## Domain Layer

### Repositories

Follow the [best practices and conventions for repositories](https://docs.abp.io/en/abp/latest/Best-Practices/Repositories) guide.

Custom repositories defined for this feature include:

- `IVisitRepository`

### Visit Manager

The `VisitManager` is used to perform some operations on the `Visit` aggregate root.

## Application Layer

### Application Services

- `VisitPublicAppService` (implements `IVisitPublicAppService`): Implements various methods for the visit feature.

## HttpApi Layer

### API Interface

- `VisitPublicController`
  API endpoint: api/cms-kit-public/visits
  Implements interfaces for adding/deleting visits.
  