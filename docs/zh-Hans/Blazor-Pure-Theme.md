# Blazor UI: Pure Theme

```json
//[doc-params]
{
    "UI": ["Blazor", "BlazorServer"]
}
```

Pure Theme 是基于 [Blazorise](https://blazorise.com/) 构建的一套 Abp Blazor 主题。

![Blazor Pure Theme](images/blazor-puretheme.jpg)

## 安装

{{if UI == "Blazor"}}

> 如果你的项目初始安装了 [Volo.Abp.AspNetCore.Components.WebAssembly.BasicTheme](https://www.nuget.org/packages/Volo.Abp.AspNetCore.Components.WebAssembly.BasicTheme) Nuget 包，请先移除。

* 将 `Dignite.Abp.AspNetCore.Components.WebAssembly.PureTheme` NuGet 包安装到 Blazor WebAssembly Web 项目中。
* 添加 `DigniteAbpAspNetCoreComponentsWebAssemblyPureThemeModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]` 属性列表中。
* 在 `ConfigureServices` 方法中，将 `Dignite.Abp.AspNetCore.Components.Web.PureTheme.Themes.Pure.App` 应用程序的根组件添加到如下位置：

    ```csharp
    var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();
    builder.RootComponents.Add<App>("#ApplicationContainer");
    ```

    `#ApplicationContainer` 是 `index.html` 中的元素 (`<div id="ApplicationContainer">Loading...</div>`)。

* 在 Blazor 项目中执行一次 [abp bundle](https://docs.abp.io/en/abp/latest/CLI#bundle)。

{{end}}

{{if UI == "BlazorServer"}}

> 如果你的项目初始安装了 [Volo.Abp.AspNetCore.Components.Server.BasicTheme](https://www.nuget.org/packages/Volo.Abp.AspNetCore.Components.Server.BasicTheme) Nuget 包，请先移除。

* 请确保已安装 [AspNetCore Pure Theme](../AspNetCore-Pure-Theme.md)。

* 将 `Dignite.Abp.AspNetCore.Components.Server.PureTheme` NuGet 包安装到 Blazor Server Web 项目中。
* 添加 `DigniteAbpAspNetCoreComponentsServerPureThemeModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]` 属性列表中。
* 在 `Pages/_Host.cshtml` 文件中执行以下更改：
  * 在页面顶部引入如下命名空间：

    ```csharp
    @using Dignite.Abp.AspNetCore.Components.Server.PureTheme.Bundling
    @using Dignite.Abp.AspNetCore.Components.Web.PureTheme.Themes.Pure
    ```

  * 在 `<head>` 标签之间添加 Pure Theme 样式：

    ```html
    <abp-style-bundle name="@BlazorPureThemeBundles.Styles.Global" />
    ```

  * 在页面的正文部分添加 Pure Theme 的 `App` 组件：

    ```html
    <component type="typeof(App)" render-mode="Server" />
    ```

  * 在 `<body>` 标签底部添加 Pure Theme Js：

    ```html
    <abp-script-bundle name="@BlazorPureThemeBundles.Scripts.Global" />
    ```

{{end}}
