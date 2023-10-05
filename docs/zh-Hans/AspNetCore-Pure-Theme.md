# ASP.NET Core MVC / Razor Pages: Pure Theme

Pure Theme 是为 ASP.NET Core MVC / Razor Pages UI 设计的主题。

## 安装

> 如果你的项目已经安装了 `Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic` NuGet 包，请先卸载它。

1. 将 `Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure` NuGet 包安装到你的 Web 项目中。
2. 在 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 的 `[DependsOn(...)]` 属性列表中添加 `DigniteAbpAspNetCoreMvcUiPureThemeModule`。
3. 在 `ConfigureServices` 方法中配置默认主题：

    ```csharp
    Configure<AbpThemingOptions>(options =>
    {
        options.DefaultThemeName = PureTheme.Name;
    });
    ```

4. 在你的 Web 项目中安装 [@abp/aspnetcore.mvc.ui.theme.basic](https://www.npmjs.com/package/@abp/aspnetcore.mvc.ui.theme.basic) NPM 包（例如：`npm install @abp/aspnetcore.mvc.ui.theme.basic` 或者 `yarn add @abp/aspnetcore.mvc.ui.theme.basic`）。
5. 运行 [abp install-libs](https://docs.abp.io/en/abp/latest/CLI#install-libs) 命令。

## 布局

### Public 布局

Pure Theme 提供了 Public 布局：

![Blazor Pure Theme](images/aspnetcore-puretheme.jpg)

在 Public 布局中，CSS 和 JS 文件经过了精简处理。以下是精简后的代码演示：

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

## 示例

请前往 [GitHub](https://github.com/dignite-projects/dignite-abp/tree/main/samples/PureTheme.BlazorServerSample) 下载示例，并按照说明运行以体验它。
