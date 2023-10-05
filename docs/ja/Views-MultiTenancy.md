# マルチテナント ビュー

ASP.NET MVC プロジェクトでは、各テナントが独自のビュー（`Views`、`Partial Views`、`Areas`、`View Components`を含む）を持つことを望むことがあります。

## インストール

> [Pure Theme](Pure-Theme.md)を使用している場合、このモジュールはすでに含まれています。

ABPプロジェクトでマルチテナント ビューの機能を実現するには、`Dignite.Abp.AspNetCore.Mvc.UI` NuGet パッケージをインストールし、プロジェクトのABPモジュールの依存リストに `[DependsOn(typeof(DigniteAbpAspNetCoreMvcUiModule))]` を追加する必要があります。

## サンプル

### `Views`の例

````csharp
public class HomeController : AbpController
{
    public IActionResult Index()
    {
        return View();
    }
}
````

テナントの `View` パス：
`~/Views/Home/Index.cshtml`

テナントの `View` パス：
`~/Tenants/{テナント名}/Views/Home/Index.cshtml`

### `Partial Views`の例

````csharp
<partial name="_toolbar" model="Model"></partial>
````

テナントの `Partial View` パス：
`~/Views/_toolbar.cshtml`

テナントの `View` パス：
`~/Tenants/{テナント名}/Views/_toolbar.cshtml`

### `Area Views`の例

````csharp
[Area("Products")]
public class HomeController : AbpController
{
    public IActionResult Index()
    {
        return View();
    }
}
````

テナントの `View` パス：
`~/Areas/Products/Views/Home/Index.cshtml`

テナントの `View` パス：
`~/Tenants/{テナント名}/Areas/Products/Views/Home/Index.cshtml`

### `View Components`の例

`/Components/MainNavbar/` ディレクトリに `MainNavbarViewComponent` という名前のビューコンポーネントを作成したと仮定しましょう：

````csharp
public class MainNavbarViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View();
    }
}
````

テナントの `View` パス：
`~/Components/MainNavbar/Default.cshtml`

テナントの `View` パス：
`~/Tenants/{テナント名}/Components/MainNavbar/Default.cshtml`

> テナントのビュー パスが見つからない場合、システムはテナントのビューが見つからない場合、自動的にマスター ビューを検索して使用します。上記のいずれのビュー タイプも正常に機能します。さらに、ASP.NET Coreの他のビューの検出機能も正常に使用できます。

## マルチテナント テーマ

`Dignite.Abp.AspNetCore.Mvc.UI` は、マルチテナント テーマの開発を簡素化するための抽象クラスである `MultiTenancyThemeBase` を提供しています。この抽象クラスは `Volo.Abp.AspNetCore.Mvc.UI.Theming.ITheme` インターフェースを実装しています。

開発者はこの抽象クラスを継承してテーマを作成し、開発コードを簡素化するだけでなく、テナントのテーマ レイアウトもサポートします。

Digniteによって開発されたASP.NET Coreのテーマ `Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure` のコードは以下のようになります：

````csharp
[ThemeName(Name)]
public class PureTheme : MultiTenancyThemeBase, ITransientDependency
{
    public const string Name = "Pure";
}
````

[Public Layout](https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.AspNetCore.Components.Web.Theming/Layout/StandardLayouts.cs) を例に挙げてみましょう：

テナントの `Layout` [Public Layout](https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.AspNetCore.Components.Web.Theming/Layout/StandardLayouts.cs) パス：
`~/Themes/Pure/Layouts/Public.cshtml`

テナントの `Layout` [Public Layout](https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.AspNetCore.Components.Web.Theming/Layout/StandardLayouts.cs) パス：
`~/Tenants/{テナント名}/Themes/Pure/Layouts/Public.cshtml`

この方法を使用すると、開発者は柔軟性と保守性を維持しながら、マルチテナントアプリケーションを簡単に作成できます。
