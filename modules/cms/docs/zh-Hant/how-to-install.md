# 如何安裝

Dignite Cms是標準的[Abp](https://docs.abp.io/en/abp/latest) 應用程式模組，通過以下方式可輕鬆集成到您的應用系統中。

## 預備要求

- 此模組依賴於 [Blob 存儲](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing) 模組來保存媒體內容。

> 確保 `BlobStoring` 模組已安裝並至少正確地配置了一個提供程序。請查閱 [文檔](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing) 以了解更多信息。

- Dignite CMS 使用 [分佈式緩存](https://docs.abp.io/zh-Hans/abp/latest/Caching) 來提高響應速度。

> 強烈建議在分佈式/集群部署中為實現數據一致性使用分佈式緩存，如 [Redis](https://docs.abp.io/zh-Hans/abp/latest/Redis-Cache)。

## 核心套件安裝

1. 將 `Dignite.Cms.Domain.Shared` NuGet 套件安裝到 `Domain.Shared` 專案中。

   將 `CmsDomainSharedModule` 添加到您的 [模組類](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 的 `[DependsOn(...)]` 屬性列表中。

2. 將 `Dignite.Cms.Domain` NuGet 套件安裝到 Domain 專案中。

   同樣，在 [模組類](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 中添加 `CmsDomainModule`。

3. 將 `Dignite.Cms.EntityFrameworkCore` NuGet 套件安裝到 Entity Framework Core 專案中。

   添加 `CmsEntityFrameworkCoreModule` 到 [模組類](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 的 `[DependsOn(...)]` 屬性列表中。

   在 `OnModelCreating()` 方法中添加以下配置：

   ```csharp
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
       base.OnModelCreating(modelBuilder);

       modelBuilder.ConfigureCms(); 
   }
   ```

   打開 Visual Studio 的套件管理器控制台，選擇 `DbMigrations` 作為預設專案，然後運行以下命令以為通知中心模組添加遷移：

   ```csharp
   add-migration Add_Cms_Module
   ```

   然後執行以下命令以更新資料庫：

   ```csharp
   update-database
   ```

## 應用程式包安裝

此模組遵循 [模組開發最佳實踐指南](https://docs.abp.io/zh-Hans/abp/latest/Best-Practices/Index)，由多個 NuGet 和 NPM 軟體包組成。如果你想了解軟體包及其之間的關係，請參閱指南。

Dignite CMS 軟體包專為各種使用場景而設計。如果您查閱了 [Dignite CMS 軟體包](https://www.nuget.org/packages?q=Dignite.Cms) 您將看到一些有 `Admin` 和 `Public` 後綴的軟體包。該模組有兩個應用程式層，原因是他們可能被用於不同類型的應用程式。

- `Dignite.Cms.Admin.*` 軟體包包括後台管理應用程式所必需的功能。
- `Dignite.Cms.Public.*` 軟體包包括前端網站上的功能。
- `Dignite.Cms.*` (不帶 Admin/Public 後綴) 軟體包稱為統一包。統一包分別是添加 Admin 和 Public (相關層的) 軟體包的快照。如果您有一個用於管理和公共網站的單應用程式，您可以使用這些軟體包。

## 網站包安裝

將 `Dignite.Cms.Public.Web` NuGet 套件安裝到 `Web` 專案中。

將 `CmsPublicWebModule` 添加到您的 [模組類](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 的 `[DependsOn(...)]` 屬性列表中。

### 配置多語言

在後台管理中已經為站點選擇了多語言，在 Web 站點中還需要配置相應的語言選項：

```csharp
Configure<AbpLocalizationOptions>(options =>
{
    options.Languages.Add(new LanguageInfo("en", "en", "English"));
    options.Languages.Add(new LanguageInfo("ja", "ja", "日本語"));
    options.Languages.Add(new LanguageInfo("zh-hans", "zh-Hans", "簡體中文"));
    options.Languages.Add(new LanguageInfo("zh-hant", "zh-Hant", "繁體中文"));
});
```

### 啟用Cms路由

在 `app.UseMultiTenancy();` 中間件後面加入 Cms 路由中間件 `app.UseCmsControllerRoute();`

```csharp
//Configuring CMS Routing
app.UseCmsControllerRoute();
```

另外為配合 Cms 路由功能，請移除 `app.UseAbpRequestLocalization();` 中間件，然後在 `app.UseCmsControllerRoute();` 中間件前面加入如下代碼：

```csharp
app.UseAbpRequestLocalization(options =>
{
    options.AddInitialRequestCultureProvider(
        new CmsRouteRequestCultureProvider()
    );
});
```

## 內部結構

### 表/集合 前綴&架構

所有表/集合使用 `Cms` 作為默認前綴。如果需要更改表的前綴或者設置一個架構名稱 (如果你的資料庫提供程序支持)，請在 `CmsDbProperties` 類中設置靜態屬性。

### 連接字符串

此模組使用 `Cms` 作為連接字符串的名稱。如果您未使用此名稱定義連接字符串，它將回退為 `Default` 連接字符串。

有關詳細信息，請參閱 [連接字符串](https://docs.abp.io/en/abp/latest/Connection-Strings) 文檔。

### HttpAPI

- 管理模組API使用 `CmsAdmin` 作為遠程服務API的名稱。如果您未使用此名稱，它將回退為 `Default` 遠程服務API的名稱。

- 公開模組API使用 `CmsPublic` 作為遠程服務API的名稱。如果您未使用此名稱，它將回退為 `Default` 遠程服務API的名稱。
