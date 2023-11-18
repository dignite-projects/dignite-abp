using Dignite.CmsKit.Features;
using Dignite.CmsKit.GlobalFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
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

    [HttpGet]
    [Route("{entityType}")]
    [Authorize]
    public async Task<ListResultDto<VisitDto>> GetListByCurrentUserAsync(string entityType, GetVisitListByCurrentUserInput input)
    {
        return await VisitPublicAppService.GetListByCurrentUserAsync(entityType,input);
    }
}
