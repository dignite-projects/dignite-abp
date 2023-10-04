# 多租户视图

在 ASP.NET MVC 项目中，我们希望每个租户都能拥有自己独立的视图，包括 `Views`、`Partial Views`、`Areas`、`View Components`。

## 安装

> 如果你使用的是[Pure Theme](Pure-Theme.md)，则已经包含了该模块。

在 ABP 项目中，可以通过安装 `Dignite.Abp.AspNetCore.Mvc.UI` NuGet 包来实现多租户视图功能。同时，需要将 `[DependsOn(typeof(DigniteAbpAspNetCoreMvcUiModule))]` 添加到项目的 ABP 模块依赖列表中。

## 示例

### `Views`示例

````csharp
public class HomeController : AbpController
{
    public IActionResult Index()
    {
        return View();
    }
}
````

租主的 `View` 路径：
`~/Views/Home/Index.cshtml`

租户的 `View` 路径：
`~/Tenants/{租户名称}/Views/Home/Index.cshtml`

### `Partial Views`示例

````csharp
<partial name="_toolbar" model="Model"></partial>
````

租主的 `Partial View` 路径：
`~/Views/_toolbar.cshtml`

租户的 `View` 路径：
`~/Tenants/{租户名称}/Views/_toolbar.cshtml`

### `Area Views`示例

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

租主的 `View` 路径：
`~/Areas/Products/Views/Home/Index.cshtml`

租户的 `View` 路径：
`~/Tenants/{租户名称}/Areas/Products/Views/Home/Index.cshtml`

### `View Components`示例

假设我们在 `/Components/MainNavbar/` 目录中创建了一个名为 `MainNavbarViewComponent` 的 View Component：

````csharp
public class MainNavbarViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View();
    }
}
````

租主的 `View` 路径：
`~/Components/MainNavbar/Default.cshtml`

租户的 `View` 路径：
`~/Tenants/{租户名称}/Components/MainNavbar/Default.cshtml`

> 如果未找到租户的视图路径，系统自动寻找并使用租主的视图，以上几种视图类型均适应。
> 除此之外，ASP.NET Core中其他视图发现功能也可以正常使用。

## 多租户主题

`Dignite.Abp.AspNetCore.Mvc.UI` 提供了一个名为 `MultiTenancyThemeBase` 的抽象类，用于简化多租户主题的开发。该抽象类实现了 `Volo.Abp.AspNetCore.Mvc.UI.Theming.ITheme` 接口。

开发者可以通过继承这个抽象类来创建主题，简化开发代码的同时，还支持租户主题布局。

由Dignite开发的ASP.NET Core 主题 `Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure`，其中主题的代码如下：

````csharp
[ThemeName(Name)]
public class PureTheme : MultiTenancyThemeBase, ITransientDependency
{
    public const string Name = "Pure";
}
````

以 [Public Layout](https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.AspNetCore.Components.Web.Theming/Layout/StandardLayouts.cs) 为例：

租主的 `Layout` [Public Layout](https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.AspNetCore.Components.Web.Theming/Layout/StandardLayouts.cs)路径：

`~/Themes/Pure/Layouts/Public.cshtml`

租户的 `Layout` [Public Layout](https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.AspNetCore.Components.Web.Theming/Layout/StandardLayouts.cs)路径：

`~/Tenants/{租户名称}/Themes/Pure/Layouts/Public.cshtml`

通过这种方式，开发者可以轻松地创建多租户应用程序，同时保持灵活性和可维护性。
