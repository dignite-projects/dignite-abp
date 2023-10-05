# ASP.NET Core MVC / Razor Pages: Pure Theme

Pure Themeは、ASP.NET Core MVC / Razor PagesのUI用のテーマです。

## インストール

> プロジェクトに初めに`Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic` NuGetパッケージがインストールされている場合は、まずそれを削除してください。

Pure ThemeをWebプロジェクトにインストールするには、次の手順に従ってください：

1. `Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure` NuGetパッケージをインストールします。

2. [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics)の`[DependsOn(...)]`属性リストに`DigniteAbpAspNetCoreMvcUiPureThemeModule`を追加します。

3. `ConfigureServices`メソッドでデフォルトのテーマを設定します：

    ```csharp
    Configure<AbpThemingOptions>(options =>
    {
        options.DefaultThemeName = PureTheme.Name;
    });
    ```

4. Webプロジェクトに[@abp/aspnetcore.mvc.ui.theme.basic](https://www.npmjs.com/package/@abp/aspnetcore.mvc.ui.theme.basic) NPMパッケージをインストールします（例：`npm install @abp/aspnetcore.mvc.ui.theme.basic`または`yarn add @abp/aspnetcore.mvc.ui.theme.basic`）。

5. [abp install-libs](https://docs.abp.io/en/abp/latest/CLI#install-libs)コマンドを実行します。

## レイアウト

### Public レイアウト

Pure ThemeにはPublicレイアウトが含まれています：

![Blazor Pure Theme](images/aspnetcore-puretheme.jpg)

Publicレイアウトでは、CSSおよびJSファイルが最小限に抑えられています。最小限に抑えられたコードは次のとおりです：

```html
<link rel="stylesheet" href="/libs/abp/core/abp.css?_v=638260250098203318" />
<link rel="stylesheet" href="/libs/bootstrap/css/bootstrap.css?_v=638260250098536014" />
<link rel="stylesheet" href="/libs/@fortawesome/fontawesome-free/css/all.css?_v=638260250098193346" />
<link rel="stylesheet" href="/libs/@fortawesome/fontawesome-free/css/v4-shims.css?_v=638260250098203318" />
<link rel="stylesheet" href="/themes/pure/public.css?_v=638315126001630796" />
```

```html
<script src="/libs/abp/utils/abp-utils.umd.min.js?_v=638207453649800000"></script>
<script src="/libs/abp/core/abp.js?_v=638260250098413048"></script>
<script src="/libs/jquery/jquery.js?_v=638260250098890227"></script>
<script src="/libs/abp/jquery/abp.jquery.js?_v=638260250236301165"></script>
<script src="/libs/bootstrap/js/bootstrap.bundle.js?_v=638260250098569464"></script>
<script src="/themes/pure/public.js?_v=638315126001630796"></script>
```

## サンプル

サンプルは、[GitHub](https://github.com/dignite-projects/dignite-abp/tree/main/samples/PureTheme.BlazorServerSample)にアクセスし、サンプルをダウンロードして指示に従って実行して体験できます。
