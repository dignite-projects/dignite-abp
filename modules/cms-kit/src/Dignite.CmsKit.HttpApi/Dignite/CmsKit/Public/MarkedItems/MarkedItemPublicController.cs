using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.CmsKit.Features;
using Volo.CmsKit.GlobalFeatures;

namespace Dignite.CmsKit.Public.MarkedItems;

[RequiresFeature(CmsKitFeatures.MarkedItemEnable)]
[RequiresGlobalFeature(typeof(MarkedItemsFeature))]
[RemoteService(Name = DigniteCmsKitPublicRemoteServiceConsts.RemoteServiceName)]
[Area(DigniteCmsKitPublicRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-public/marked-items")]
public class MarkedItemPublicController : CmsKitPublicControllerBase, IMarkedItemPublicAppService
{
    public MarkedItemPublicController(IMarkedItemPublicAppService markedItemPublicAppService)
    {
        MarkedItemPublicAppService = markedItemPublicAppService;
    }

    protected IMarkedItemPublicAppService MarkedItemPublicAppService { get; }

    [HttpGet]
    [Route("{entityType}")]
    public async Task<ListResultDto<string>> GetListForUserAsync([NotNull] string entityType)
    {
        return await MarkedItemPublicAppService.GetListForUserAsync(entityType); 
    }
}
