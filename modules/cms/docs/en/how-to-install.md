# Installation Guide

Dignite Cms is a standard [Abp](https://docs.abp.io/en/abp/latest) application module, and it can be easily integrated into your application system through the following steps.

## Prerequisites

- This module depends on the [Blob Storing](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing) module to store media content.

    > Make sure that the `BlobStoring` module is installed and at least one provider is correctly configured. Please refer to the [documentation](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing) for more information.

- Dignite CMS uses [Distributed Caching](https://docs.abp.io/zh-Hans/abp/latest/Caching) to improve response speed.

    > It is strongly recommended to use distributed caching for achieving data consistency in distributed/clustered deployments, such as using [Redis](https://docs.abp.io/zh-Hans/abp/latest/Redis-Cache).

## Core Package Installation

1. Install the `Dignite.Cms.Domain.Shared` NuGet package into the `Domain.Shared` project.

   Then add `CmsDomainSharedModule` to the `[DependsOn(...)]` attribute list of your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

2. Install the `Dignite.Cms.Domain` NuGet package into the Domain project.

   Similarly, add `CmsDomainModule` to the [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

3. Install the `Dignite.Cms.EntityFrameworkCore` NuGet package into the Entity Framework Core project.

   Add `CmsEntityFrameworkCoreModule` to the `[DependsOn(...)]` attribute list of your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

   Add the following configuration to the `OnModelCreating()` method:

   ```csharp
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
       base.OnModelCreating(modelBuilder);

       modelBuilder.ConfigureCms(); 
   }
   ```

   Open the Package Manager Console in Visual Studio, select `DbMigrations` as the default project, and then run the following command to add a migration for the CMS module:

   ```csharp
   add-migration Add_Cms_Module
   ```

   Then execute the following command to update the database:

   ```csharp
   update-database
   ```

## Application Package Installation

This module follows the [Best Practices for Module Development](https://docs.abp.io/zh-Hans/abp/latest/Best-Practices/Index) and consists of multiple NuGet and NPM packages. If you want to understand the packages and their relationships, please refer to the guide.

Dignite CMS packages are designed for various usage scenarios. If you browse the [Dignite CMS packages](https://www.nuget.org/packages?q=Dignite.Cms), you will see some packages with `Admin` and `Public` suffixes. The module has two application layers because they may be used in different types of applications.

- `Dignite.Cms.Admin.*` packages include the functionality required for the admin dashboard application.
- `Dignite.Cms.Public.*` packages include the functionality on the frontend website.
- `Dignite.Cms.*` (without Admin/Public suffix) packages are called unified packages. Unified packages are snapshots of adding Admin and Public (related layer) packages. If you have a single application for both admin and public websites, you can use these packages.

## Website Package Installation

Install the `Dignite.Cms.Public.Web` NuGet package into the `Web` project.

Add `CmsPublicWebModule` to the `[DependsOn(...)]` attribute list of your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

### Configure Localization

Multiple languages have been selected for the site in the admin dashboard, and the corresponding language options need to be configured in the Web site:

```csharp
Configure<AbpLocalizationOptions>(options =>
{
    options.Languages.Add(new LanguageInfo("en", "en", "English"));
    options.Languages.Add(new LanguageInfo("ja", "ja", "日本語"));
    options.Languages.Add(new LanguageInfo("zh-hans", "zh-Hans", "简体中文"));
    options.Languages.Add(new LanguageInfo("zh-hant", "zh-Hant", "繁體中文"));
});
```

### Enable CMS Routing

Add the CMS routing middleware `app.UseCmsControllerRoute();` after the `app.UseMultiTenancy();` middleware.

```csharp
//Configuring CMS Routing
app.UseCmsControllerRoute();
```

Additionally, to work with the CMS routing feature, please remove the `app.UseAbpRequestLocalization();` middleware and then add the following code befor the `app.UseCmsControllerRoute();` middleware:

```csharp
app.UseAbpRequestLocalization(options =>
{
    options.AddInitialRequestCultureProvider(
        new CmsRouteRequestCultureProvider()
    );
});
```

## Internal Structure

### Table/Collection Prefix & Schema

All tables/collections use `Cms` as the default prefix. If you need to change the table prefix or set a schema name (if supported by your database provider), please set static properties in the `CmsDbProperties` class.

### Connection String

This module uses `Cms` as the connection string name. If you haven't defined a connection string with this name, it will fall back to the `Default` connection string.

For more information, please refer to the [Connection Strings](https://docs.abp.io/en/abp/latest/Connection-Strings) documentation.

### HttpAPI

- The API for the admin module uses `CmsAdmin` as the name of the remote service API. If you haven't used this name, it will fall back to the `Default` remote service API name.

- The API for the public module uses `CmsPublic` as the name of the remote service API. If you haven't used this name, it will fall back to the `Default` remote service API name.
