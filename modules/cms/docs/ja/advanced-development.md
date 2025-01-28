# 高度開発

## フィールドによるエントリのクエリ

### `cms-entry-list`

`cms-entry-list` コンポーネントには、フィールド値によるエントリのクエリを行うためのパラメーター (`querying-by-fields`) があります。これは `QueryingByField` クラスのインスタンスのリストで、それぞれの `QueryingByField` インスタンスには次の2つのパラメーターが含まれています。

- `Name`: フィールドの名前。
- `Value`: クエリに使用されるフィールド値。フィールドのタイプによって、値は異なる形式を取ります。

  - `TextFieldQuerying`: フィールドが指定された `Value` を含むかどうかでフィルタリングします。
  - `SwitchFieldQuerying`: `Value` は `bool` に変換可能である必要があり、その値が指定された `Value` と等しいかどうかでフィルタリングします。
  - `NumericFieldQuerying`: `Value` は `minValue-maxValue` としてフォーマットされ、フィールド値が `minValue` より大きく `maxValue` より小さいかどうかでフィルタリングします。
  - `SelectFieldQuerying`: `Value` は `Guid` 値のコンマ区切りのリストであり、その値が `Value` の `Guid` 値のいずれかを含むかどうかでフィルタリングします。
  - `EntryFieldQuerying`: `Value` は `Guid` 値のコンマ区切りのリストであり、その値が `Value` の `Guid` 値のいずれかを含むかどうかでフィルタリングします。

    > 将来のバージョンでは、さらに多くのクエリ方法がサポートされる予定です。

### `GetListAsync(GetEntriesInput input)` メソッド

`IEntryPublicAppService` の `GetListAsync(GetEntriesInput input)` メソッドは、`QueryingByFieldsJson` という名前の文字列パラメーターを受け入れます。これは `QueryingByField` オブジェクトのシリアル化されたリストを表します。

実際には、内部的に `cms-entry-list` コンポーネントは `QueryingByField` オブジェクトのリストを JSON にシリアル化し、それを `IEntryPublicAppService` の `GetListAsync(GetEntriesInput input)` メソッドの `QueryingByFieldsJson` パラメーターに渡します。

## ドメイン名で現在のテナントを解決する

Dignite Cms は、ドメイン名によって現在のテナントを決定する機能を提供します：

1. `Dignite.Cms.AspNetCore.MultiTenancy` NuGet パッケージを `Web Site` プロジェクトにインストールします。

   `CmsAspNetCoreMultiTenancyModule` を [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` 属性リストに追加します。

2. `Web Site` の `Module`ファイルに以下の設定を追加します：

```csharp
Configure<AbpTenantResolveOptions>(options =>
{
    // ドメイン名で現在のテナントを解決します
    options.AddCmsDomainTenantResolver();
});
```

現在のテナントを決定する公式ドキュメントについては、こちらを参照してください： [Determining the Current Tenant](https://abp.io/docs/latest/framework/architecture/multi-tenancy#determining-the-current-tenant)
