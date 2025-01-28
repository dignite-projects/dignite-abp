# インストールガイド

Dignite Cmsは、標準の[Abp](https://docs.abp.io/en/abp/latest)アプリケーションモジュールであり、以下の手順に従って簡単にアプリケーションシステムに統合できます。

## 必要条件

- このモジュールは、メディアコンテンツを保存するための[Blob Storing](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing)モジュールに依存しています。

    > `BlobStoring`モジュールがインストールされ、少なくとも1つのプロバイダが正しく構成されていることを確認してください。詳細については、[ドキュメント](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing)を参照してください。

- Dignite CMSは、レスポンス速度を向上させるために[Distributed Caching](https://docs.abp.io/zh-Hans/abp/latest/Caching)を使用しています。

    > データの一貫性を確保するために、Redisなどの分散/クラスタ化されたデプロイメントで分散キャッシュを使用することを強くお勧めします。

## コアパッケージのインストール

1. `Dignite.Cms.Domain.Shared` NuGetパッケージを`Domain.Shared`プロジェクトにインストールします。

   次に、[モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics)の`[DependsOn(...)]`属性リストに`CmsDomainSharedModule`を追加します。

2. `Dignite.Cms.Domain` NuGetパッケージをDomainプロジェクトにインストールします。

   同様に、[モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics)に`CmsDomainModule`を追加します。

3. `Dignite.Cms.EntityFrameworkCore` NuGetパッケージをEntity Framework Coreプロジェクトにインストールします。

   `[DependsOn(...)]`属性リストに`CmsEntityFrameworkCoreModule`を追加します。

   次の構成を`OnModelCreating()`メソッドに追加します：

   ```csharp
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
       base.OnModelCreating(modelBuilder);

       modelBuilder.ConfigureCms(); 
   }
   ```

   Visual Studioのパッケージマネージャーコンソールを開き、デフォルトプロジェクトとして`DbMigrations`を選択し、次のコマンドを実行してCMSモジュールのマイグレーションを追加します：

   ```csharp
   add-migration Add_Cms_Module
   ```

   次に、データベースを更新するために次のコマンドを実行します：

   ```csharp
   update-database
   ```

## アプリケーションパッケージのインストール

このモジュールは、[モジュール開発のベストプラクティス](https://docs.abp.io/zh-Hans/abp/latest/Best-Practices/Index)に従っており、複数のNuGetおよびNPMパッケージから構成されています。パッケージとその関係を理解したい場合は、ガイドを参照してください。

Dignite CMSパッケージは、さまざまな使用シナリオに対応するように設計されています。[Dignite CMSパッケージ](https://www.nuget.org/packages?q=Dignite.Cms)を参照すると、`Admin`および`Public`接尾辞を持ついくつかのパッケージが表示されます。モジュールには2つのアプリケーションレイヤがあるため、異なる種類のアプリケーションで使用される可能性があります。

- `Dignite.Cms.Admin.*` パッケージには、管理ダッシュボードアプリケーションに必要な機能が含まれています。
- `Dignite.Cms.Public.*` パッケージには、フロントエンドのウェブサイトに関する機能が含まれています。
- `Dignite.Cms.*`（Admin/Public接尾辞なし）パッケージは、統一パッケージと呼ばれます。統一パッケージは、AdminとPublic（関連するレイヤ）パッケージを追加したスナップショットです。管理とパブリックの両方のウェブサイトに単一のアプリケーションを持っている場合、これらのパッケージを使用できます。

## ウェブサイトパッケージのインストール

`Web`プロジェクトに`Dignite.Cms.Public.Web` NuGetパッケージをインストールします。

[モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics)の`[DependsOn(...)]`属性リストに`CmsPublicWebModule`を追加します。

### ローカライゼーションの設定

管理ダッシュボードでサイトに複数の言語が選択されている場合、対応する言語オプションをWebサイトで設定する必要があります：

```csharp
Configure<AbpLocalizationOptions>(options =>
{
    options.Languages.Add(new LanguageInfo("en",

 "en", "English"));
    options.Languages.Add(new LanguageInfo("ja", "ja", "日本語"));
    options.Languages.Add(new LanguageInfo("zh-hans", "zh-Hans", "简体中文"));
    options.Languages.Add(new LanguageInfo("zh-hant", "zh-Hant", "繁體中文"));
});
```

### CMSルーティングの有効化

`app.UseMultiTenancy();`の後にCMSルーティングミドルウェア`app.UseCmsControllerRoute();`を追加します。

```csharp
//Configuring CMS Routing
app.UseCmsControllerRoute();
```

さらに、CMSルーティング機能と連携するために、`app.UseAbpRequestLocalization();`ミドルウェアを削除し、`app.UseCmsControllerRoute();`ミドルウェアの先に次のコードを追加します：

```csharp
app.UseAbpRequestLocalization(options =>
{
    options.AddInitialRequestCultureProvider(
        new CmsRouteRequestCultureProvider()
    );
});
```

## 内部構造

### テーブル/コレクションプレフィックス＆スキーマ

すべてのテーブル/コレクションは、デフォルトのプレフィックスとして`Cms`を使用します。テーブルプレフィックスを変更したり、スキーマ名を設定したりする場合は、`CmsDbProperties`クラスの静的プロパティを設定してください。

### 接続文字列

このモジュールでは、接続文字列の名前として`Cms`を使用しています。この名前で接続文字列を定義していない場合は、`Default`接続文字列にフォールバックします。

詳細については、[接続文字列](https://docs.abp.io/en/abp/latest/Connection-Strings)ドキュメントを参照してください。

### HttpAPI

- 管理モジュールのAPIは、リモートサービスAPIの名前として`CmsAdmin`を使用します。この名前を使用していない場合は、`Default`リモートサービスAPI名にフォールバックします。

- パブリックモジュールのAPIは、リモートサービスAPIの名前として`CmsPublic`を使用します。この名前を使用していない場合は、`Default`リモートサービスAPI名にフォールバックします。
