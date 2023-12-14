# Cms Kit

````json
//[doc-params]
{
    "DB": ["EF", "Mongo"]
}
````

此模块是基于 [Abp Cms Kit](https://docs.abp.io/zh-Hans/abp/latest/Modules/Cms-Kit/Index) 开发，增加了以下几个功能：

* 提供 [**喜欢**](Favourite.md) 功能，用户添加对任何资源的喜欢/收藏功能.
* 提供 [**访问**](Visit.md) 功能，记录对任何资源的访问功能.

## 安装

* 将 `Dignite.CmsKit.Domain.Shared` Nuget 包安装到 `Domain.Shared` 项目中

    添加 `DigniteCmsKitDomainSharedModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

* 将 `Dignite.CmsKit.Domain` Nuget 包安装到 Domain 项目中

    添加 `DigniteCmsKitDomainModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

{{if DB == "EF"}}

* 将 `Dignite.CmsKit.EntityFrameworkCore` Nuget 包安装到 EntityFrameworkCore 项目中

    添加 `DigniteCmsKitEntityFrameworkCoreModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

    添加 `builder.ConfigureCmsKit()` 到 `OnModelCreating()` 方法中:

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

    打开 Visual Studio 的 包管理控制台 选择 `DbMigrations` 做为默认项目. 然后编写以下命令为文档模块添加迁移.

    ```csharp
    add-migration Added_DigniteCmsKit_Module
    ```

    现在更新数据库

    ```csharp
    update-database
    ```

{{end}}

{{if DB == "Mongo"}}

* 将 `Dignite.CmsKit.MongoDB` Nuget 包安装到 MongoDB 项目中

    添加 `DigniteCmsKitEntityMongoDBModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

    添加 `builder.ConfigureCmsKit()` 到 `OnModelCreating()` 方法中:

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

* 将 `Dignite.CmsKit.Application.Contracts` Nuget 包安装到 Application.Contracts 项目中

    添加 `DigniteCmsKitApplicationContractsModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

* 将 `Dignite.CmsKit.Application` Nuget 包安装到 Application 项目中

    添加 `DigniteCmsKitApplicationModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

* 将 `Dignite.CmsKit.HttpApi` Nuget 包安装到 HttpApi 项目中

    添加 `DigniteCmsKitHttpApiModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

## 如何使用

> 默认情况下, Dignite Cms-Kit `GlobalFeature` 被禁用. 因此初始迁移将为空. 所以, 当你使用 EF Core 安装时,你可以添加 `--skip-db-migrations` 命令来跳过迁移. 启用 Dignite Cms-Kit 全局功能后, 请添加新的迁移.

安装过程完成后, 在您的解决方案 `Domain.Shared` 项目中打开 `GlobalFeatureConfigurator` 类, 并将以下代码写入 `Configure` 方法中, 以启用 CMS Kit 模块的全部功能.

```csharp
GlobalFeatureManager.Instance.Modules.DigniteCmsKit(cmsKit =>
{
    cmsKit.EnableAll();
});
```

你可能更愿意逐个启用这些功能, 而不是启用全部功能. 以下示例仅启用了 [喜欢](Favourite.md) 和 [访问记录](Visit.md) 功能:

````csharp
GlobalFeatureManager.Instance.Modules.DigniteCmsKit(cmsKit =>
{
    cmsKit.Favourites.Enable();
    cmsKit.Visits.Enable();
});
````

> 如果你使用 EF Core, 不要忘记添加一个新的迁移并更新你的数据库.

## 内部结构

### 表/集合 前缀&架构

与 [Abp Cms Kit](https://docs.abp.io/zh-Hans/abp/latest/Modules/Cms-Kit/Index) 一样，本模块所有表/集合使用 `Cms` 作为默认前缀.

### 连接字符串

与 [Abp Cms Kit](https://docs.abp.io/zh-Hans/abp/latest/Modules/Cms-Kit/Index) 一样，本模块使用 `CmsKit` 作为连接字符串的名称. 如果您未使用此名称定义连接字符串, 它将回退为 `Default` 连接字符串.

有关详细信息, 请参阅 [连接字符串](https://docs.abp.io/en/abp/latest/Connection-Strings) 文档.
