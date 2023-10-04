# ASP.NET Core MVC / Razor Pages: Pure Theme

Pure Theme 是ASP.NET Core MVC / Razor Pages UI的主题。

## 安装

> 如果你的项目初始安装了 `Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic` Nuget 包，请先移除.

* 将 `Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure` NuGet 包安装到 Web 项目中。
* 添加 `DigniteAbpAspNetCoreMvcUiPureThemeModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中.
* 在`ConfigureServices` 方法配置默认主题:

    ```csharp
    Configure<AbpThemingOptions>(options =>
    {
        options.DefaultThemeName = PureTheme.Name;
    });
    ```

* 安装 [@abp/aspnetcore.mvc.ui.theme.basic](https://www.npmjs.com/package/@abp/aspnetcore.mvc.ui.theme.basic) NPM 包到web项目 (e.g. `npm install @abp/aspnetcore.mvc.ui.theme.basic` 或者 `yarn add @abp/aspnetcore.mvc.ui.theme.basic`).

* 运行 [abp install-libs](https://docs.abp.io/en/abp/latest/CLI#install-libs).

## 布局

### Public 布局

为Pure Theme 创建了 Public 布局。

![Blazor Pure Theme](images/aspnetcore-puretheme.jpg)

Public 布局中精简了CSS文件和JS文件，精简后代码演示：

````html
<link rel="stylesheet" href="/libs/abp/core/abp.css?_v=638260250098203318" />
<link rel="stylesheet" href="/libs/bootstrap/css/bootstrap.css?_v=638260250098536014" />
<link rel="stylesheet" href="/libs/@fortawesome/fontawesome-free/css/all.css?_v=638260250098193346" />
<link rel="stylesheet" href="/libs/@fortawesome/fontawesome-free/css/v4-shims.css?_v=638260250098203318" />
<link rel="stylesheet" href="/themes/pure/public.css?_v=638315126001630796" />
````

````html
<script   src="/libs/abp/utils/abp-utils.umd.min.js?_v=638207453649800000"></script>
<script   src="/libs/abp/core/abp.js?_v=638260250098413048"></script>
<script   src="/libs/jquery/jquery.js?_v=638260250098890227"></script>
<script   src="/libs/abp/jquery/abp.jquery.js?_v=638260250236301165"></script>
<script   src="/libs/bootstrap/js/bootstrap.bundle.js?_v=638260250098569464"></script>
<script   src="/themes/pure/public.js?_v=638315126001630796"></script>
````

## 示例

请前往 [Github](https://github.com/dignite-projects/dignite-abp/tree/main/samples/PureTheme.BlazorServerSample)下载示例，并按说明运行体验。
