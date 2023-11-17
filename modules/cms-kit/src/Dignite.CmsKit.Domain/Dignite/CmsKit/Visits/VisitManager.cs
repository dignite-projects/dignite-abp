using System;
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

    public async Task<Visit> CreateAsync(Guid userId, string entityType, string entityId)
    {
        if (!await VisitDefinitionStore.IsDefinedAsync(entityType))
        {
            throw new EntityCantHaveVisitException(entityType);
        }

        var visit = await VisitRepository.GetVisitAsync(entityType, entityId, userId);
        if (visit != null)
        {
            return visit;
        }

        return await VisitRepository.InsertAsync(
            new Visit(
                GuidGenerator.Create(),
                entityType,
                entityId,
                userId,
                CurrentTenant.Id
            )
        );
    }
}
