using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;

namespace Dignite.CmsKit.Favourites;
public class FavouriteSynchronizer :
        IDistributedEventHandler<EntityCreatedEto<FavouriteEto>>,
        ITransientDependency
{

    public Task HandleEventAsync(EntityCreatedEto<FavouriteEto> eventData)
    {
        var favourite = eventData.Entity;
        return Task.CompletedTask;
    }
}