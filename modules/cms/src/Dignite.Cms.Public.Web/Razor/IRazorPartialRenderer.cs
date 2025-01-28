using System.Threading.Tasks;

namespace Dignite.Cms.Public.Web.Razor
{
    public interface IRazorPartialRenderer
    {
        Task<string> RenderAsync<TModel>(string partialName, TModel model);
    }
}
