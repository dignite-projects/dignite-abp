using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace Dignite.CmsKit.Visits;

public class Visit : BasicAggregateRoot<Guid>, IHasCreationTime,IMayHaveCreator
{
    public virtual Guid? TenantId { get; protected set; }

    public virtual string EntityType { get; protected set; }

    public virtual string EntityId { get; protected set; }

    public string UserAgent { get; protected set; }

    public string ClientIpAddress { get; protected set; }

    /// <summary>
    /// Duration the length of seconds a user browsing
    /// </summary>
    public int Duration { get; protected set; }

    public virtual Guid? CreatorId { get; protected set; }

    public virtual DateTime CreationTime { get; set; }

    protected Visit()
    {

    }

    public Visit(
        Guid id,
        [NotNull] string entityType,
        [NotNull] string entityId,
        string userAgent,
        string clientIpAddress,
        int duration,
        Guid? creatorId,
        Guid? tenantId = null
    )
        : base(id)
    {
        EntityType = Check.NotNullOrWhiteSpace(entityType, nameof(entityType), VisitConsts.MaxEntityTypeLength);
        EntityId = Check.NotNullOrWhiteSpace(entityId, nameof(entityId), VisitConsts.MaxEntityIdLength);
        UserAgent = userAgent;
        ClientIpAddress = clientIpAddress;
        Duration = duration;
        CreatorId = creatorId;
        TenantId = tenantId;
    }
}
