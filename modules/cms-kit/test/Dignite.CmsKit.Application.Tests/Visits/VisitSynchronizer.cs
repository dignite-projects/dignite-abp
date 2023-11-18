using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;

namespace Dignite.CmsKit.Visits;
public class VisitSynchronizer :
        IDistributedEventHandler<EntityCreatedEto<VisitEto>>,
        ITransientDependency
{

    public Task HandleEventAsync(EntityCreatedEto<VisitEto> eventData)
    {
        var visit = eventData.Entity;
        return Task.CompletedTask;
    }
}