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

In the Public Layout, CSS and JS files are minimized. Here's the code after minimization:

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

## Examples

For examples, please visit [GitHub](https://github.com/dignite-projects/dignite-abp/tree/main/samples/PureTheme.BlazorServerSample), download the sample, and follow the instructions to run and experience it.
