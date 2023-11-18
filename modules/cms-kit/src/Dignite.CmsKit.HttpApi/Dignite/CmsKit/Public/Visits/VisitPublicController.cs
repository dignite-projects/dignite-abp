using System.Threading.Tasks;
using Dignite.CmsKit.Features;
using Dignite.CmsKit.GlobalFeatures;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;

namespace Dignite.CmsKit.Public.Visits;

[RequiresFeature(CmsKitFeatures.VisitEnable)]
[RequiresGlobalFeature(typeof(VisitsFeature))]
[RemoteService(Name = DigniteCmsKitPublicRemoteServiceConsts.RemoteServiceName)]
[Area(DigniteCmsKitPublicRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-public/visits")]
public class VisitPublicController : CmsKitPublicControllerBase, IVisitPublicAppService
{
    protected IVisitPublicAppService VisitPublicAppService { get; }

    public VisitPublicController(IVisitPublicAppService visitPublicAppService)
    {
        VisitPublicAppService = visitPublicAppService;
    }

    [HttpPost]
    [Route("{entityType}/{entityId}")]
    public async Task<VisitDto> CreateAsync(string entityType, string entityId, CreateVisitInput input)
    {
        return await VisitPublicAppService.CreateAsync(entityType, entityId, input);
    }
}
