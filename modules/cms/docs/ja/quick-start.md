# Dignite Cms管理システムの起動ガイド

Dignite Cms管理システムはBlazor技術をベースにしており、Blazor WebAssemblyとBlazor Serverの2つの実行モードを提供しています。以下は、管理システムを起動するための詳細な手順です：

````json
//[doc-params]
{
    "UI": ["Blazor","BlazorServer"]
}
````

{{if UI == "Blazor"}}

## Blazor WebAssemblyでの開始

1. **IdentityServerの起動**

    ターミナルで`host\Dignite.Cms.IdentityServer`ディレクトリに移動し、次のコマンドを実行します：

    ```bash
    dotnet ef database update
    ```

    ```bash
    dotnet run
    ```

2. **HttpApi.Hostの起動**

    ターミナルで`host\Dignite.Cms.HttpApi.Host`ディレクトリに移動し、次のコマンドを実行します：

    ```bash
    dotnet ef database update
    ```

    ```bash
    abp install-libs
    ```

    ```bash
    dotnet run
    ```

    > 初回実行時にはシードデータが自動的に作成されます。

3. **Blazor.Hostの起動**

    ターミナルで`host\Dignite.Cms.Blazor.Host`ディレクトリに移動し、次のコマンドを実行します：

    ```bash
    dotnet run
    ````

    ブラウザで `https://localhost:44307` アドレスにアクセスして、Dignite Cms管理システムにアクセスします。

    > 初期ユーザー名: admin
    >
    > 初期パスワード: 1q2w3E*

4. **MVCウェブサイトの起動**

    ターミナルで`host\Dignite.Cms.Web.Host`ディレクトリに移動し、次のコマンドを実行します：

    ```bash
    abp install-libs
    ```

    ```bash
    dotnet run
    ```

    ブラウザで `https://localhost:44339` アドレスにアクセスして、Dignite Cms MVCウェブサイトにアクセスします。

{{end}}

{{if UI == "BlazorServer"}}

## Blazor Serverでの開始

ターミナルで`host\Dignite.Cms.Blazor.Server.Host`ディレクトリに移動し、次のコマンドを実行します：

```bash
dotnet ef database update
```

```bash
dotnet run
```

ブラウザで `https://localhost:44361` アドレスにアクセスして、Dignite Cms管理システムにアクセスします。

{{end}}

## 機能の概要

### フィールド管理

フィールドはエントリのプロパティを定義するために使用され、システムは便宜上いくつかのプリセットフィールドを提供します。

![フィールド管理 スクリーンショット](images/fields.png)

### セクション管理

セクションはサイトの骨格であり、エントリの構造的レイアウトをサポートするために使用されます。システムは一般的に使用されるいくつかのセクションを自動的に作成します。

![セクション管理 スクリーンショット](images/sections.png)

### エントリタイプの管理

エントリタイプは、セクションの下にあるエントリに適用されるフィールドを定義するために使用され、各セクションに複数のエントリタイプを構成できます。

![エントリタイプの構成 スクリーンショット](images/entry-type-edit.png)

### エントリの管理

エントリはウェブサイト上のさまざまなセクションのコンテンツであり、マルチ

言語およびマルチバージョンの機能をサポートしています。

![エントリリスト スクリーンショット](images/entry-list.png)

![エントリ編集ページ スクリーンショット](images/entry-edit.png)
