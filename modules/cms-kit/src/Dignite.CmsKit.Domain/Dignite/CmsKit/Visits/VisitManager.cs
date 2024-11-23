using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Dignite.CmsKit.Visits;

public class VisitManager : DomainService
{
    protected IVisitRepository VisitRepository { get; }
    protected IVisitEntityTypeDefinitionStore VisitDefinitionStore { get; }

    public VisitManager(
        IVisitRepository visitRepository,
        IVisitEntityTypeDefinitionStore visitDefinitionStore)
    {
        VisitRepository = visitRepository;
        VisitDefinitionStore = visitDefinitionStore;
    }

    public async Task<Visit> CreateAsync(
        string entityType,
        string entityId,
        string? browserInfo,
        string? deviceInfo,
        string? clientIpAddress,
        int duration)
    {
        if (!await VisitDefinitionStore.IsDefinedAsync(entityType))
        {
            throw new EntityCantHaveVisitException(entityType);
        }

        return await VisitRepository.InsertAsync(
            new Visit(
                GuidGenerator.Create(),
                entityType,
                entityId,
                browserInfo,
                deviceInfo,
                clientIpAddress,
                duration,
                CurrentTenant.Id
            )
        );
    }
}
