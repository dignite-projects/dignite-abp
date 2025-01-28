# 如何安装

Dignite Cms是标准的[Abp](https://docs.abp.io/en/abp/latest) 应用程序模块，通过以下方式可轻松集成到您的应用系统中。

## 预备要求

- 此模块依赖于 [Blob 存储](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing) 模块来保存媒体内容。

> 确保 `BlobStoring` 模块已安装并至少正确地配置了一个提供程序. 请查阅 [文档](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing) 了解更多信息。

- Dignite CMS 使用 [分布式缓存](https://docs.abp.io/zh-Hans/abp/latest/Caching) 来提高响应速度。
  
> 强烈建议在分布式/集群部署中为实现数据一致性使用分布式缓存, 如 [Redis](https://docs.abp.io/zh-Hans/abp/latest/Redis-Cache)。

## 核心包安装

1. 将 `Dignite.Cms.Domain.Shared` NuGet 包安装到 `Domain.Shared` 项目中。

   将 `CmsDomainSharedModule` 添加到您的 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 的 `[DependsOn(...)]` 属性列表中。

2. 将 `Dignite.Cms.Domain` NuGet 包安装到 Domain 项目中。

   同样，在 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 中添加 `CmsDomainModule`。

3. 将 `Dignite.Cms.EntityFrameworkCore` NuGet 包安装到 Entity Framework Core 项目中。

   添加 `CmsEntityFrameworkCoreModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 的 `[DependsOn(...)]` 属性列表中。

   在 `OnModelCreating()` 方法中添加以下配置：

   ```csharp
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
       base.OnModelCreating(modelBuilder);

       modelBuilder.ConfigureCms(); 
   }
   ```

   打开 Visual Studio 的包管理控制台，选择 `DbMigrations` 作为默认项目，然后运行以下命令以为通知中心模块添加迁移：

   ```csharp
   add-migration Add_Cms_Module
   ```

   然后执行以下命令以更新数据库：

   ```csharp
   update-database
   ```

## 应用包安装

此模块遵循 [模块开发最佳实践指南](https://docs.abp.io/zh-Hans/abp/latest/Best-Practices/Index), 由多个 NuGet 和 NPM 软件包组成. 如果你想了解软件包及其之间的关系, 请参阅指南.

Dignite CMS 软件包专为各种使用场景而设计。 如果您查阅了 [Dignite CMS 软件包](https://www.nuget.org/packages?q=Dignite.Cms) 您将看到一些有 `Admin` 和 `Public` 后缀的软件包. 该模块有两个应用程序层, 原因是他们可能被用于不同类型的应用程序。

- `Dignite.Cms.Admin.*` 软件包包括后台管理应用程序所必须的功能。
- `Dignite.Cms.Public.*` 软件包包括前端网站上的功能。
- `Dignite.Cms.*` (不带 Admin/Public 后缀) 软件包称为统一包. 统一包分别是添加 Admin 和 Public (相关层的) 软件包的快照. 如果您有一个用于管理和公共网站的单应用程序, 您可以使用这些软件包.

## 网站包安装

将 `Dignite.Cms.Public.Web` NuGet 包安装到 `Web` 项目中。

将 `CmsPublicWebModule` 添加到您的 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 的 `[DependsOn(...)]` 属性列表中。

### 配置多语言

在后台管理中已经为站点选择了多语言，在Web站点中还需要配置相应的语言选项：

```csharp
Configure<AbpLocalizationOptions>(options =>
{
    options.Languages.Add(new LanguageInfo("en", "en", "English"));
    options.Languages.Add(new LanguageInfo("ja", "ja", "日本語"));
    options.Languages.Add(new LanguageInfo("zh-hans", "zh-Hans", "简体中文"));
    options.Languages.Add(new LanguageInfo("zh-hant", "zh-Hant", "繁體中文"));
});
```

### 启用Cms路由

在`app.UseMultiTenancy();`中间件后面加入Cms路由中间件`app.UseCmsControllerRoute();`

```csharp
//Configuring CMS Routing
app.UseCmsControllerRoute();
```

另外为配合Cms路由中当前语言，请移除`app.UseAbpRequestLocalization();`中间件，然后在`app.UseCmsControllerRoute();`中间件前面加入如下代码：

```csharp
app.UseAbpRequestLocalization(options =>
{
    options.AddInitialRequestCultureProvider(
        new CmsRouteRequestCultureProvider()
    );
});
```

## 内部结构

### 表/集合 前缀&架构

所有表/集合使用 `Cms` 作为默认前缀。如果需要更改表的前缀或者设置一个架构名称 (如果你的数据库提供程序支持), 请在 `CmsDbProperties` 类中设置静态属性。

### 连接字符串

此模块使用 `Cms` 作为连接字符串的名称。如果您未使用此名称定义连接字符串, 它将回退为 `Default` 连接字符串。

有关详细信息, 请参阅 [连接字符串](https://docs.abp.io/en/abp/latest/Connection-Strings) 文档。

### HttpAPI

- 管理模块API使用 `CmsAdmin` 作为远程服务API的名称。如果您未使用此名称, 它将回退为 `Default` 远程服务API的名称。

- 公开模块API使用 `CmsPublic` 作为远程服务API的名称。如果您未使用此名称, 它将回退为 `Default` 远程服务API的名称。
