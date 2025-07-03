# 通知中心模块

```json
//[doc-params]
{
    "DB": ["EF", "Mongo"]
}
```

通知中心模块是 Abp 应用程序模块，可轻松集成到您的应用系统中，以实现通知的发布和接收功能。

## 安装

1. 将 `Dignite.Abp.NotificationCenter.Domain.Shared` NuGet 包安装到 `Domain.Shared` 项目中。

   将 `AbpNotificationCenterDomainSharedModule` 添加到您的 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 的 `[DependsOn(...)]` 属性列表中。

2. 将 `Dignite.Abp.NotificationCenter.Domain` NuGet 包安装到 Domain 项目中。

   同样，在 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 中添加 `AbpNotificationCenterDomainModule`。

3. 如果使用 Entity Framework Core（EF），则将 `Dignite.Abp.NotificationCenter.EntityFrameworkCore` NuGet 包安装到 Entity Framework Core 项目中。

   添加 `AbpNotificationCenterEntityFrameworkCoreModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 的 `[DependsOn(...)]` 属性列表中。

   在 `OnModelCreating()` 方法中添加以下配置：

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
       modelBuilder.ConfigureNotificationCenter(); // 添加此行以配置 NotificationCenter 模块
   }
   ```

   打开 Visual Studio 的包管理控制台，选择 `DbMigrations` 作为默认项目，然后运行以下命令以为通知中心模块添加迁移：

   ```csharp
   add-migration Added_NotificationCenter_Module
   ```

   然后执行以下命令以更新数据库：

   ```csharp
   update-database
   ```

4. 如果使用 MongoDB，将 `Dignite.Abp.NotificationCenter.MongoDB` NuGet 包安装到 MongoDB 项目中。

   添加 `AbpNotificationCenterMongoDbModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 的 `[DependsOn(...)]` 属性列表中。

   同样，在 `OnModelCreating()` 方法中添加以下配置：

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
       modelBuilder.ConfigureNotificationCenter(); // 添加此行以配置 NotificationCenter 模块
   }
   ```

5. 将 `Dignite.Abp.Notifications.Identity` NuGet 包安装到 Domain 项目中。

   同样，在 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 中添加 `AbpNotificationsIdentityModule`。

6. 将 `Dignite.Abp.NotificationCenter.Application.Contracts` NuGet 包安装到 Application.Contracts 项目中。

   添加 `AbpNotificationCenterApplicationContractsModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 的 `[DependsOn(...)]` 属性列表中。

7. 将 `Dignite.Abp.NotificationCenter.Application` NuGet 包安装到 Application 项目中。

   添加 `AbpNotificationCenterApplicationModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 的 `[DependsOn(...)]` 属性列表中。

8. 将 `Dignite.Abp.NotificationCenter.HttpApi` NuGet 包安装到 HttpApi 项目中。

   添加 `AbpNotificationCenterHttpApiModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 的 `[DependsOn(...)]` 属性列表中。
