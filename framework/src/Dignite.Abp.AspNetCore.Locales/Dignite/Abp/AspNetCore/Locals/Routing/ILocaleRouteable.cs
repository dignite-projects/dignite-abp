namespace Dignite.Abp.AspNetCore.Locals.Routing;

/// <summary>
/// <see cref="ILocaleRouteable"/> 是一个标记接口，用于指示页面是否需要通过 <see cref="LocaleRouteDataRequestCultureProvider"/> 进行区域化。
/// 只有实现了该接口的页面才会触发 <see cref="LocaleRouteDataRequestCultureProvider.DetermineProviderCultureResult(Microsoft.AspNetCore.Http.HttpContext)"/> 方法的执行，从而优化性能。
/// </summary>
/// <example>
/// Example of use in Razor Pages
/// <code>
/// public class ProductPage : PageModel, ILocaleRouteable 
/// {
///     ...
/// }
/// </code>
/// 
/// Example of use in MVC
/// <code>
/// public class HomeController : Controller, ILocaleRouteable
/// {
///     ...
/// }
/// </code>
/// </example>
public interface ILocaleRouteable
{
}
