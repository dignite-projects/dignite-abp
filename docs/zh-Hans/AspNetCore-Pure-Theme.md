# ASP.NET Core MVC / Razor Pages: Pure Theme

Pure Theme 是为 ASP.NET Core MVC / Razor Pages UI 设计的主题。

## 安装

1. 将 `Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure` NuGet 包安装到你的 Web 项目中。
2. 在 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 的 `[DependsOn(...)]` 属性列表中添加 `AbpAspNetCoreMvcUiPureThemeModule`。
3. 在 `ConfigureServices` 方法中配置默认主题：

    ```csharp
    Configure<AbpThemingOptions>(options =>
    {
        options.DefaultThemeName = PureTheme.Name;
    });
    ```

4. 在你的 Web 项目中安装 [@abp/aspnetcore.mvc.ui.theme.shared](https://www.npmjs.com/package/@abp/aspnetcore.mvc.ui.theme.shared) NPM 包（例如：`npm install @abp/aspnetcore.mvc.ui.theme.shared` 或者 `yarn add @abp/aspnetcore.mvc.ui.theme.shared`）。
5. 运行 [abp install-libs](https://docs.abp.io/en/abp/latest/CLI#install-libs) 命令。