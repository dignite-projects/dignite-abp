# Notification Center Module

```json
//[doc-params]
{
    "DB": ["EF", "Mongo"]
}
```

The Notification Center Module is an Abp application module that can be easily integrated into your application system to facilitate the publishing and receiving of notifications.

## Installation

1. Install the `Dignite.Abp.NotificationCenter.Domain.Shared` NuGet package in the `Domain.Shared` project.

   Add `AbpNotificationCenterDomainSharedModule` to the `[DependsOn(...)]` attribute list in your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

2. Install the `Dignite.Abp.NotificationCenter.Domain` NuGet package in the Domain project.

   Similarly, add `AbpNotificationCenterDomainModule` to your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

3. If you are using Entity Framework Core (EF), install the `Dignite.Abp.NotificationCenter.EntityFrameworkCore` NuGet package in the Entity Framework Core project.

   Add `AbpNotificationCenterEntityFrameworkCoreModule` to your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

   Add the `builder.ConfigureNotificationCenter()` to the `OnModelCreating()` method:

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
       modelBuilder.ConfigureNotificationCenter(); // Add this line to configure the NotificationCenter Module
   }
   ```

   Open the Package Manager Console in Visual Studio, select `DbMigrations` as the default project, and write the following command to add a migration for the NotificationCenter module:

   ```csharp
   add-migration Added_NotificationCenter_Module
   ```

   Now update the database by running:

   ```csharp
   update-database
   ```

4. If you are using MongoDB, install the `Dignite.Abp.NotificationCenter.MongoDB` NuGet package in the MongoDB project.

   Add `AbpNotificationCenterMongoDbModule` to your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

   Similarly, add the `builder.ConfigureNotificationCenter()` to the `OnModelCreating()` method:

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
       modelBuilder.ConfigureNotificationCenter(); // Add this line to configure the NotificationCenter Module
   }
   ```

5. Install the `Dignite.Abp.Notifications.Identity` NuGet package in the Domain project.

   Similarly, add `AbpNotificationsIdentityModule` to your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

6. Install the `Dignite.Abp.NotificationCenter.Application.Contracts` NuGet package in the Application.Contracts project.

   Add `AbpNotificationCenterApplicationContractsModule` to your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

7. Install the `Dignite.Abp.NotificationCenter.Application` NuGet package in the Application project.

   Add `AbpNotificationCenterApplicationModule` to your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

8. Install the `Dignite.Abp.NotificationCenter.HttpApi` NuGet package in the HttpApi project.

   Add `AbpNotificationCenterHttpApiModule` to your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).
