# Cms Kit

````json
//[doc-params]
{
    "DB": ["EF", "Mongo"]
}
````

This module is developed based on [Abp Cms Kit](https://docs.abp.io/en/abp/latest/Modules/Cms-Kit/Index) and adds the following features:

* Provides [**Visit**](Visit.md) functionality, recording visits to any resource.

## Installation

* Install the `Dignite.CmsKit.Domain.Shared` NuGet package in the `Domain.Shared` project.

    Add `DigniteCmsKitDomainSharedModule` to the `[DependsOn(...)]` property list in the [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

* Install the `Dignite.CmsKit.Domain` NuGet package in the Domain project.

    Add `DigniteCmsKitDomainModule` to the `[DependsOn(...)]` property list in the [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

{{if DB == "EF"}}

* Install the `Dignite.CmsKit.EntityFrameworkCore` NuGet package in the EntityFrameworkCore project.

    Add `DigniteCmsKitEntityFrameworkCoreModule` to the `[DependsOn(...)]` property list in the [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

    Add `builder.ConfigureCmsKit()` to the `OnModelCreating()` method:

    ```csharp
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigurePermissionManagement();
        modelBuilder.ConfigureSettingManagement();
        modelBuilder.ConfigureAuditLogging();
        modelBuilder.ConfigureIdentity();
        modelBuilder.ConfigureFeatureManagement();
        modelBuilder.ConfigureTenantManagement();
        modelBuilder.ConfigureDigniteCmsKit(); //Add this line to configure the CmsKit Module
    }
    ```

    Open the Package Manager Console in Visual Studio, select `DbMigrations` as the default project, and then write the following command to add migrations for the document module.

    ```csharp
    add-migration Added_DigniteCmsKit_Module
    ```

    Now update the database.

    ```csharp
    update-database
    ```

{{end}}

{{if DB == "Mongo"}}

* Install the `Dignite.CmsKit.MongoDB` NuGet package in the MongoDB project.

    Add `DigniteCmsKitEntityMongoDBModule` to the `[DependsOn(...)]` property list in the [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

    Add `builder.ConfigureCmsKit()` to the `OnModelCreating()` method:

    ```csharp
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigurePermissionManagement();
        modelBuilder.ConfigureSettingManagement();
        modelBuilder.ConfigureAuditLogging();
        modelBuilder.ConfigureIdentity();
        modelBuilder.ConfigureFeatureManagement();
        modelBuilder.ConfigureTenantManagement();
        modelBuilder.ConfigureDigniteCmsKit(); //Add this line to configure the Dignite CmsKit Module
    }
    ```

{{end}}

* Install the `Dignite.CmsKit.Application.Contracts` NuGet package in the Application.Contracts project.

    Add `DigniteCmsKitApplicationContractsModule` to the `[DependsOn(...)]` property list in the [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

* Install the `Dignite.CmsKit.Application` NuGet package in the Application project.

    Add `DigniteCmsKitApplicationModule` to the `[DependsOn(...)]` property list in the [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

* Install the `Dignite.CmsKit.HttpApi` NuGet package in the HttpApi project.

    Add `DigniteCmsKitHttpApiModule` to the `[DependsOn(...)]` property list in the [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

## How to Use

> By default, the Dignite Cms-Kit `GlobalFeature` is disabled. Therefore, the initial migration will be empty. So, when installing with EF Core, you can add the `--skip-db-migrations` command to skip migrations. After enabling Dignite Cms-Kit global features, please add a new migration.

Once the installation process is complete, open the `GlobalFeatureConfigurator` class in your solution's `Domain.Shared` project and write the following code in the `Configure` method to enable all features of the CMS Kit module.

```csharp
GlobalFeatureManager.Instance.Modules.DigniteCmsKit(cmsKit =>
{
    cmsKit.EnableAll();
});
```

You may prefer to enable these features one by one rather than enabling all at once. The following example only enables the [Visit](Visit.md) features:

````csharp
GlobalFeatureManager.Instance.Modules.DigniteCmsKit(cmsKit =>
{
    cmsKit.Visits.Enable();
});
````

> If you are using EF Core, don't forget to add a new migration and update your database.

## Internal Structure

### Table/Collection Prefix & Schema

Similar to [Abp Cms Kit](https://docs.abp.io/en/abp/latest/Modules/Cms-Kit/Index), all tables/collections in this module use `Cms` as the default prefix.

### Connection String

Similar to [Abp Cms Kit](https://docs.abp.io/en/abp/latest/Modules/Cms-Kit/Index), this module uses `CmsKit` as the name of the connection string. If you haven't defined a connection string with this name, it will fall back to the `Default` connection string.

For more details, refer to the [Connection Strings](https://docs.abp.io/en/abp/latest/Connection-Strings) documentation.
