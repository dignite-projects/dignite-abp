# 多テナントのローカライゼーション

多テナントのローカライゼーションは、各テナントが独自のローカライゼーションを持つことができる機能で、ウェブサイトのデザインなど、各テナントの異なるニーズに合わせたローカライズされたコンテンツを提供する典型的なシナリオに適しています。

## インストール

1. `Dignite.Abp.Localization` NuGet パッケージを Web プロジェクトにインストールします。
2. `[DependsOn(...)]` 属性リストに `DigniteAbpLocalizationModule` を追加します。

## ローカライゼーション リソース ファイルの作成

`/Localization/` フォルダー内にローカライゼーション リソース ファイル クラスを作成し、そのクラスに `MultiTenancyLocalizationResourceName` 属性を追加します。たとえば:

```csharp
[MultiTenancyLocalizationResourceName("CmsResource")]
public class CmsResource
{
}
```

テナントごとの JSON ファイルは `/Tenants/Localization/` フォルダーにあります。テナントの名前に基づいたフォルダーが `/Tenants/{テナント名}/Localization/` に作成されます。

JSON ローカライゼーション ファイルの内容は次のようになります:

```json
{
  "HelloWorld": "Hello World!"
}
```

## ローカライゼーション テキストの取得

### クラス内でのシンプルな使用法

単に `IStringLocalizer<TResource>` サービスを注入し、次のように使用します:

```csharp
public class MyService : ITransientDependency
{
    private readonly IStringLocalizer<CmsResource> _localizer;

    public MyService(IStringLocalizer<CmsResource> localizer)
    {
        _localizer = localizer;
    }

    public void Foo()
    {
        var str = _localizer["HelloWorld"];
    }
}
```

### Razor ビュー/ページでの使用

Razor ビュー/ページで `IStringLocalizer<TResource>` サービスを注入します:

```csharp
@inject IStringLocalizer<CmsResource> _localizer

<h1>@_localizer["HelloWorld"]</h1>
```

### フォーマット パラメータ

フォーマット パラメータはローカライゼーション キーの後に渡すことができます。たとえば、メッセージが `Hello {0}, welcome!` の場合、`{0}` パラメータをローカライザに渡すことができます: `_localizer["HelloMessage", "John"]`。

> ローカライゼーションの詳細については、[Microsoft のローカライゼーション ドキュメント](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization)を参照してください。
