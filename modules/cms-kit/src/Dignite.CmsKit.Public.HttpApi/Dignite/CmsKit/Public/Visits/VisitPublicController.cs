using System;
using System.Threading.Tasks;
using Dignite.CmsKit.Features;
using Dignite.CmsKit.GlobalFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.WebClientInfo;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;

namespace Dignite.CmsKit.Public.Visits;

[RequiresFeature(CmsKitFeatures.VisitEnable)]
[RequiresGlobalFeature(typeof(VisitsFeature))]
[RemoteService(Name = CmsKitPublicRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitPublicRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-public/visits")]
public class VisitPublicController : CmsKitPublicControllerBase, IVisitPublicAppService
{
    protected IVisitPublicAppService VisitPublicAppService { get; }
    protected IWebClientInfoProvider WebClientInfoProvider { get; }

    public VisitPublicController(IVisitPublicAppService visitPublicAppService, IWebClientInfoProvider webClientInfoProvider)
    {
        VisitPublicAppService = visitPublicAppService;
        WebClientInfoProvider = webClientInfoProvider;
    }

    [HttpPost]
    [Route("{entityType}/{entityId}")]
    public async Task<VisitDto> CreateAsync(string entityType, string entityId, CreateVisitInput input)
    {
        var clientIpAddress = WebClientInfoProvider.ClientIpAddress;
        var browserInfo = WebClientInfoProvider.BrowserInfo;
        var deviceInfo = WebClientInfoProvider.DeviceInfo;
        if (clientIpAddress.IsNullOrWhiteSpace())
        {
            input.ClientIpAddress = clientIpAddress;
        }
        if (browserInfo.IsNullOrWhiteSpace())
        {
            input.BrowserInfo = browserInfo;
            input.DeviceInfo = deviceInfo;
        }
        return await VisitPublicAppService.CreateAsync(entityType, entityId, input);
    }

    [HttpGet]
    [Route("{entityType}")]
    public async Task<ListResultDto<string>> GetListForUserAsync(string entityType, int skipCount = 0, int maxResultCount = 100)
    {
        return await VisitPublicAppService.GetListForUserAsync(entityType,skipCount,maxResultCount);
    }
}
