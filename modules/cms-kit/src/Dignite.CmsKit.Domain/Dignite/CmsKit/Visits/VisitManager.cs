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

    public async Task<Visit> CreateAsync(
        string entityType,
        string entityId,
        string userAgent,
        string clientIpAddress,
        int duration,
        Guid? userId)
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
                userAgent,
                clientIpAddress,
                duration,
                userId,
                CurrentTenant.Id
            )
        );
    }
}
