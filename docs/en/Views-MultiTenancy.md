# Multi-Tenant Views

In ASP.NET MVC projects, it is often desired that each tenant has its own independent views, including `Views`, `Partial Views`, `Areas`, and `View Components`.

## Installation

> If you are using the [Pure Theme](Pure-Theme.md), this module is already included.

In ABP projects, you can achieve multi-tenant views functionality by installing the `Dignite.Abp.AspNetCore.Mvc.UI` NuGet package. Additionally, you need to add `[DependsOn(typeof(DigniteAbpAspNetCoreMvcUiModule))]` to the dependency list of your project's ABP module.

## Example

### Example for `Views`

```csharp
public class HomeController : AbpController
{
    public IActionResult Index()
    {
        return View();
    }
}
```

Path for the view for the host:
`~/Views/Home/Index.cshtml`

Path for the view for the tenant:
`~/Tenants/{TenantName}/Views/Home/Index.cshtml`

### Example for `Partial Views`

```csharp
<partial name="_toolbar" model="Model"></partial>
```

Path for the partial view for the host:
`~/Views/_toolbar.cshtml`

Path for the partial view for the tenant:
`~/Tenants/{TenantName}/Views/_toolbar.cshtml`

### Example for `Area Views`

```csharp
[Area("Products")]
public class HomeController : AbpController
{
    public IActionResult Index()
    {
        return View();
    }
}
```

Path for the view for the host:
`~/Areas/Products/Views/Home/Index.cshtml`

Path for the view for the tenant:
`~/Tenants/{TenantName}/Areas/Products/Views/Home/Index.cshtml`

### Example for `View Components`

Let's assume we have a View Component named `MainNavbarViewComponent` in the directory `/Components/MainNavbar/`:

```csharp
public class MainNavbarViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View();
    }
}
```

Path for the view for the host:
`~/Components/MainNavbar/Default.cshtml`

Path for the view for the tenant:
`~/Tenants/{TenantName}/Components/MainNavbar/Default.cshtml`

> If the view path for the tenant is not found, the system automatically falls back to using the host's view. This applies to all the mentioned view types above. Additionally, other view discovery features in ASP.NET Core also work as expected.

## Multi-Tenant Themes

`Dignite.Abp.AspNetCore.Mvc.UI` provides an abstract class called `MultiTenancyThemeBase` to simplify the development of multi-tenant themes. This abstract class implements the `Volo.Abp.AspNetCore.Mvc.UI.Theming.ITheme` interface.

Developers can create themes by inheriting from this abstract class, simplifying development while also supporting tenant-specific theme layouts.

The code for the ASP.NET Core theme developed by Dignite, `Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure`, is as follows:

```csharp
[ThemeName(Name)]
public class PureTheme : MultiTenancyThemeBase, ITransientDependency
{
    public const string Name = "Pure";
}
```

As an example using the [Public Layout](https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.AspNetCore.Components.Web.Theming/Layout/StandardLayouts.cs):

Path for the layout for the host:
`~/Themes/Pure/Layouts/Public.cshtml`

Path for the layout for the tenant:
`~/Tenants/{TenantName}/Themes/Pure/Layouts/Public.cshtml`

This approach allows developers to easily create multi-tenant applications while maintaining flexibility and maintainability.
