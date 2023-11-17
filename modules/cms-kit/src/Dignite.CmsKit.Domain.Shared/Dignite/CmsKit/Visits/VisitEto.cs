using System;
using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;

namespace Dignite.CmsKit.Visits;

[EventName("Dignite.CmsKit.Visits.Visit")]
[Serializable]
public class VisitEto : IMultiTenant
{
    public Guid Id { get; protected set; }
    public virtual Guid? TenantId { get; protected set; }

    public virtual string EntityType { get; protected set; }

    public virtual string EntityId { get; protected set; }

    public string UserAgent { get; protected set; }

    public string ClientIpAddress { get; protected set; }

    /// <summary>
    /// Represents the length of time a user browsing
    /// </summary>
    public int Duration { get; protected set; }

    public virtual Guid? CreatorId { get; set; }

    public virtual DateTime CreationTime { get; set; }
}
