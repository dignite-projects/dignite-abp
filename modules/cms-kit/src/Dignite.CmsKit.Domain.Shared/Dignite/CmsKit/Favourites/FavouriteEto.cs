using System;
using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;

namespace Dignite.CmsKit.Favourites;

[EventName("Dignite.CmsKit.Favourites.Favourite")]
[Serializable]
public class FavouriteEto: IMultiTenant
{
    public Guid Id { get; protected set; }
    public virtual Guid? TenantId { get; protected set; }

    public virtual string EntityType { get; protected set; }

    public virtual string EntityId { get; protected set; }

    public virtual Guid CreatorId { get; protected set; }
}
