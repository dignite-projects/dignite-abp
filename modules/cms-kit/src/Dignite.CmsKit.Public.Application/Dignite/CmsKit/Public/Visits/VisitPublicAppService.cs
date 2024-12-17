using System.Threading.Tasks;
using Dignite.CmsKit.Visits;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;

namespace Dignite.CmsKit.Public.Visits;
public class VisitPublicAppService : CmsKitPublicAppServiceBase, IVisitPublicAppService
{
    protected IVisitRepository VisitRepository { get; }
    protected VisitManager VisitManager { get; }

    public VisitPublicAppService(IVisitRepository visitRepository, VisitManager visitManager)
    {
        VisitRepository = visitRepository;
        VisitManager = visitManager;
    }

    public async Task<VisitDto> CreateAsync(string entityType, string entityId, CreateVisitInput input)
    {
        var visit = await VisitManager.CreateAsync(entityType, entityId,input.BrowserInfo, input.DeviceInfo,input.ClientIpAddress,input.Duration);

        return ObjectMapper.Map<Visit, VisitDto>(visit);
    }

    [Authorize]
    public async Task<ListResultDto<string>> GetListForUserAsync(string entityType, int skipCount = 0, int maxResultCount = 100)
    {
        var list = await VisitRepository.GetEntityIdsFilteredByUserAsync(CurrentUser.Id.Value,entityType,skipCount,maxResultCount);
        return new ListResultDto<string>(list);
    }
}
