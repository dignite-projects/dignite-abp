using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.CmsKit.Public.Visits;
public class VisitDto:EntityDto<Guid>
{
    public virtual string EntityType { get;  set; }

    public virtual string EntityId { get;  set; }

    public virtual string? BrowserInfo { get; set; }

    public virtual string? DeviceInfo { get; set; }

    public virtual string? ClientIpAddress { get; set; }

    /// <summary>
    /// Duration the length of seconds a user browsing
    /// </summary>
    public int Duration { get; set; }

    public virtual Guid? CreatorId { get; set; }

    public virtual DateTime CreationTime { get; set; }
}
