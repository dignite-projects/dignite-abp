# 訪問機能

Dignite CMS Kitは、ユーザーが任意のリソースにアクセスした履歴を記録する**訪問**機能を提供しています。

## 訪問機能を有効にする

始める前に、Dignite Cms Kitのすべての機能を有効にしていない場合は、**訪問**機能を別途有効にする必要があります：

解決策の `Domain.Shared` プロジェクトで `GlobalFeatureConfigurator` クラスを開き、以下のコードを `Configure` メソッドに追加します。

````csharp
GlobalFeatureManager.Instance.Modules.DigniteCmsKit(cmsKit =>
{
    cmsKit.Visits.Enable();
});
````

## オプション

**訪問**機能は、エンティティの種類ごとに訪問をグループ化するメカニズムを提供します。たとえば、ユーザーがどの商品を**訪問**したかを記録したい場合は、`Product` という名前のエンティティタイプを定義し、そのエンティティタイプの下に訪問を追加する必要があります。

`CmsKitVisitOptions` は `Domain` で構成でき、[モジュール](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `ConfigureServices` メソッドで構成します。たとえば：

```csharp
Configure<CmsKitVisitOptions>(options =>
{
    options.EntityTypes.Add(new VisitEntityTypeDefinition("Product"));
});
```

## ドメイン層

### リポジトリ

[リポジトリの最適な慣習と規約](https://docs.abp.io/en/abp/latest/Best-Practices/Repositories) ガイドに従います。

この機能用に定義されたカスタムリポジトリには以下が含まれます：

- `IVisitRepository`

### 訪問マネージャ

`VisitManager` は `Visit` 集約ルートでいくつかの操作を実行するために使用されます。

## アプリケーション層

### アプリケーションサービス

- `VisitPublicAppService`（`IVisitPublicAppService` を実装）：訪問機能のさまざまなメソッドを実装します。

## HttpApi層

### API インターフェース

- `VisitPublicController`
  API エンドポイント：api/cms-kit-public/visits
  訪問の追加/削除のためのインターフェースを実装します。
  