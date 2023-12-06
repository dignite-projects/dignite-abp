# Pure Theme for ASP.NET Core MVC / Razor Pages

Pure Theme is a theme for ASP.NET Core MVC / Razor Pages UI.

## Installation

> If your project initially installed the `Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic` NuGet package, please remove it first.

To install the Pure Theme in your web project, follow these steps:

1. Install the `Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure` NuGet package.

2. Add `DigniteAbpAspNetCoreMvcUiPureThemeModule` to the `[DependsOn(...)]` attribute list in your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

3. Configure the default theme in the `ConfigureServices` method:

    ```csharp
    Configure<AbpThemingOptions>(options =>
    {
        options.DefaultThemeName = PureTheme.Name;
    });
    ```

4. Install the [@abp/aspnetcore.mvc.ui.theme.basic](https://www.npmjs.com/package/@abp/aspnetcore.mvc.ui.theme.basic) NPM package in your web project (e.g., `npm install @abp/aspnetcore.mvc.ui.theme.basic` or `yarn add @abp/aspnetcore.mvc.ui.theme.basic`).

5. Run [abp install-libs](https://docs.abp.io/en/abp/latest/CLI#install-libs).

## Layout

### Public Layout

The Pure Theme includes a Public Layout:

![Blazor Pure Theme](images/aspnetcore-puretheme.jpg)

## Examples

For examples, please visit [GitHub](https://github.com/dignite-projects/dignite-abp/tree/main/samples/PureTheme.BlazorServerSample), download the sample, and follow the instructions to run and experience it.
