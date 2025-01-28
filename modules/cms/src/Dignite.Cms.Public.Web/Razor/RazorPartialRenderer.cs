using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Volo.Abp.DependencyInjection;

namespace Dignite.Cms.Public.Web.Razor
{
    public class RazorPartialRenderer : IRazorPartialRenderer, ITransientDependency
    {
        private readonly IRazorViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IHttpContextAccessor _contextAccessor;
        public RazorPartialRenderer(
                IRazorViewEngine viewEngine,
                ITempDataProvider tempDataProvider,
                IHttpContextAccessor contextAccessor)
        {
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _contextAccessor = contextAccessor;
        }
        public async Task<string> RenderAsync<TModel>(string partialName, TModel model)
        {
            var actionContext = new ActionContext(_contextAccessor.HttpContext, _contextAccessor.HttpContext.GetRouteData(), new ActionDescriptor());
            var partial = FindView(actionContext, partialName);
            using (var output = new StringWriter())
            {
                var viewContext = new ViewContext(
                        actionContext,
                        partial,
                        new ViewDataDictionary<TModel>(
                                metadataProvider: new EmptyModelMetadataProvider(),
                                modelState: new ModelStateDictionary())
                        {
                            Model = model
                        },
                        new TempDataDictionary(
                                actionContext.HttpContext,
                                _tempDataProvider),
                        output,
                        new HtmlHelperOptions()
                );
                await partial.RenderAsync(viewContext);
                return output.ToString();
            }
        }
        private IView FindView(ActionContext actionContext, string partialName)
        {
            var partialResult = _viewEngine.GetView(null, partialName, false);
            if (partialResult.Success)
            {
                return partialResult.View;
            }
            var findPartialResult = _viewEngine.FindView(actionContext, partialName, false);
            if (findPartialResult.Success)
            {
                return findPartialResult.View;
            }
            var searchedLocations = partialResult.SearchedLocations.Concat(findPartialResult.SearchedLocations);
            var errorMessage = string.Join(
                    Environment.NewLine,
                    new[] { $"Unable to find partial '{partialName}'. The following locations were searched:" }.Concat(searchedLocations)); ;
            throw new InvalidOperationException(errorMessage);
        }
    }
}
