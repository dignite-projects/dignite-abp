# 通知中心模块

````json
//[doc-params]
{
    "DB": ["EF", "Mongo"]
}
````

通知中心模块是Abp 应用模块，可以方便插入到你的应用系统中，实现发布、接收通知。

## 安装

- 将 `Dignite.Abp.NotificationCenter.Domain.Shared` Nuget 包安装到 `Domain.Shared` 项目中

    添加 `DigniteAbpNotificationCenterDomainSharedModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

- 将 `Dignite.Abp.NotificationCenter.Domain` Nuget 包安装到 Domain 项目中

    添加 `DigniteAbpNotificationCenterDomainModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

- 将 `Dignite.Abp.Notifications.Identity` Nuget 包安装到 Domain 项目中

    添加 `DigniteAbpNotificationsIdentityModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

{{if DB == "EF"}}

- 将 `Dignite.Abp.NotificationCenter.EntityFrameworkCore` Nuget 包安装到 EntityFrameworkCore 项目中

    添加 `DigniteAbpNotificationCenterEntityFrameworkCoreModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

    添加 `builder.ConfigureNotificationCenter()` 到 `OnModelCreating()` 方法中:

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
        modelBuilder.ConfigureNotificationCenter(); //Add this line to configure the NotificationCenter Module
    }
    ```

    打开 Visual Studio 的 包管理控制台 选择 `DbMigrations` 做为默认项目. 然后编写以下命令为文档模块添加迁移.

    ```csharp
    add-migration Added_NotificationCenter_Module
    ```

    现在更新数据库

    ```csharp
    update-database
    ```

{{end}}

{{if DB == "Mongo"}}

- 将 `Dignite.Abp.NotificationCenter.MongoDB` Nuget 包安装到 MongoDB 项目中

    添加 `DigniteAbpNotificationCenterMongoDbModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

    添加 `builder.ConfigureNotificationCenter()` 到 `OnModelCreating()` 方法中:

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
        modelBuilder.ConfigureNotificationCenter(); //Add this line to configure the NotificationCenter Module
    }
    ```

{{end}}

- 将 `Dignite.Abp.NotificationCenter.Application.Contracts` Nuget 包安装到 Application.Contracts 项目中

    添加 `DigniteAbpNotificationCenterApplicationContractsModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

- 将 `Dignite.Abp.NotificationCenter.Application` Nuget 包安装到 Application 项目中

    添加 `DigniteAbpNotificationCenterApplicationModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

- 将 `Dignite.Abp.NotificationCenter.HttpApi` Nuget 包安装到 HttpApi 项目中

    添加 `DigniteAbpNotificationCenterHttpApiModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。
