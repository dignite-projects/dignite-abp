# Cms Kit

````json
//[doc-params]
{
    "DB": ["EF", "Mongo"]
}
````

このモジュールは [Abp Cms Kit](https://docs.abp.io/en/abp/latest/Modules/Cms-Kit/Index) をベースに開発され、以下の機能が追加されています:

* [**お気に入り**](Favourite.md) 機能を提供し、ユーザーが任意のリソースにお気に入り/コレクションを追加できます。
* [**訪問**](Visit.md) 機能を提供し、任意のリソースへの訪問を記録します。

## インストール

* `Domain.Shared` プロジェクトに `Dignite.CmsKit.Domain.Shared` NuGet パッケージをインストールしてください。

    [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` プロパティリストに `DigniteCmsKitDomainSharedModule` を追加します。

* Domain プロジェクトに `Dignite.CmsKit.Domain` NuGet パッケージをインストールしてください。

    [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` プロパティリストに `DigniteCmsKitDomainModule` を追加します。

{{if DB == "EF"}}

* EntityFrameworkCore プロジェクトに `Dignite.CmsKit.EntityFrameworkCore` NuGet パッケージをインストールしてください。

    [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` プロパティリストに `DigniteCmsKitEntityFrameworkCoreModule` を追加します。

    `OnModelCreating()` メソッドに `builder.ConfigureCmsKit()` を追加します:

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
        modelBuilder.ConfigureDigniteCmsKit(); // この行を追加して CmsKit モジュールを構成します
    }
    ```

    Visual Studio のパッケージ管理コンソールを開き、`DbMigrations` をデフォルトプロジェクトとして選択し、以下のコマンドを文書モジュールにマイグレーションを追加します。

    ```csharp
    add-migration Added_DigniteCmsKit_Module
    ```

    そして、データベースを更新します。

    ```csharp
    update-database
    ```

{{end}}

{{if DB == "Mongo"}}

* MongoDB プロジェクトに `Dignite.CmsKit.MongoDB` NuGet パッケージをインストールしてください。

    [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` プロパティリストに `DigniteCmsKitEntityMongoDBModule` を追加します。

    `OnModelCreating()` メソッドに `builder.ConfigureCmsKit()` を追加します:

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
        modelBuilder.ConfigureDigniteCmsKit(); // この行を追加して Dignite CmsKit モジュールを構成します
    }
    ```

{{end}}

* Application.Contracts プロジェクトに `Dignite.CmsKit.Application.Contracts` NuGet パッケージをインストールしてください。

    [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` プロパティリストに `DigniteCmsKitApplicationContractsModule` を追加します。

* Application プロジェクトに `Dignite.CmsKit.Application` NuGet パッケージをインストールしてください。

    [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` プロパティリストに `DigniteCmsKitApplicationModule` を追加します。

* HttpApi プロジェクトに `Dignite.CmsKit.HttpApi` NuGet パッケージをインストールしてください。

    [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` プロパティリストに `DigniteCmsKitHttpApiModule` を追加します。

## 使い方

> デフォルトでは、Dignite Cms-Kit の `GlobalFeature` は無効です。そのため、初回のマイグレーションは空になります。したがって、EF Core でインストールする際には、`--skip-db-migrations` コマンドを追加してマイグレーションをスキップすることができます。Dignite Cms-Kit のグローバル機能を有効にした後、新しいマイグレーションを追加してください。

インストールプロセスが完了したら、解決策の `Domain.Shared` プロジェクトで `GlobalFeatureConfigurator` クラスを開き、以下のコードを `Configure` メソッドに書き込んで CMS Kit モジュールのすべての機能を有効にします。

```csharp
GlobalFeatureManager.Instance.Modules.DigniteCmsKit(cmsKit =>
{
    cmsKit.EnableAll();
});
```

全ての機能を一度に有効にするのでは
