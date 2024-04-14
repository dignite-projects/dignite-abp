using System.Threading.Tasks;

namespace Dignite.Abp.AspNetCore.Mvc.Razor;

public interface IRazorPartialRenderer
{
    Task<string> RenderAsync<TModel>(string partialName, TModel model);
}
