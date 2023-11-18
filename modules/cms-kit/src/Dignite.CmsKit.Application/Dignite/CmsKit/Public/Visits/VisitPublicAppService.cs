using System.Threading.Tasks;
using Dignite.CmsKit.Visits;
using Volo.CmsKit;

namespace Dignite.CmsKit.Public.Visits;
public class VisitPublicAppService : CmsKitAppServiceBase, IVisitPublicAppService
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
        var visit = await VisitManager.CreateAsync(entityType, entityId,input.UserAgent,input.ClientIpAddress,input.Duration, CurrentUser.Id);

        return ObjectMapper.Map<Visit, VisitDto>(visit);
    }
}
