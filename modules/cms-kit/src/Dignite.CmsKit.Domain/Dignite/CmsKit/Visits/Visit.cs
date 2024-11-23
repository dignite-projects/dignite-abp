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

    public virtual string? BrowserInfo { get; protected set; }

    public virtual string? DeviceInfo { get; protected set; }

    public virtual string? ClientIpAddress { get; protected set; }

    /// <summary>
    /// Duration the length of seconds a user browsing
    /// </summary>
    public virtual int Duration { get; protected set; }

    public virtual Guid? CreatorId { get; protected set; }

    public virtual DateTime CreationTime { get; set; }

    protected Visit()
    {

    }

    public Visit(
        Guid id,
        [NotNull] string entityType,
        [NotNull] string entityId,
        string? browserInfo,
        string? deviceInfo,
        string? clientIpAddress,
        int duration,
        Guid? tenantId = null
    )
        : base(id)
    {
        EntityType = Check.NotNullOrWhiteSpace(entityType, nameof(entityType), VisitConsts.MaxEntityTypeLength);
        EntityId = Check.NotNullOrWhiteSpace(entityId, nameof(entityId), VisitConsts.MaxEntityIdLength);
        BrowserInfo = browserInfo;
        DeviceInfo = deviceInfo;
        ClientIpAddress = clientIpAddress;
        Duration = duration;
        TenantId = tenantId;
    }
}
