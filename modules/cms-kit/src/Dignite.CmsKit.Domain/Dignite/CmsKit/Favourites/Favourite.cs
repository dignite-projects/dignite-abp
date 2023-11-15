using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace Dignite.CmsKit.Favourites;

public class Favourite : BasicAggregateRoot<Guid>, IHasCreationTime, IMustHaveCreator
{
    public virtual Guid? TenantId { get; protected set; }

    public virtual string EntityType { get; protected set; }

    public virtual string EntityId { get; protected set; }

    public virtual Guid CreatorId { get; set; }

    public virtual DateTime CreationTime { get; set; }

    protected Favourite()
    {

    }

    public Favourite(
        Guid id,
        [NotNull] string entityType,
        [NotNull] string entityId,
        Guid creatorId,
        Guid? tenantId = null
    )
        : base(id)
    {
        EntityType = Check.NotNullOrWhiteSpace(entityType, nameof(entityType), FavouriteConsts.MaxEntityTypeLength);
        EntityId = Check.NotNullOrWhiteSpace(entityId, nameof(entityId), FavouriteConsts.MaxEntityIdLength);
        CreatorId = creatorId;
        TenantId = tenantId;
    }
}
