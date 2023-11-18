using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.CmsKit.Public.Visits;
public class VisitDto:EntityDto<Guid>
{
    public virtual string EntityType { get;  set; }

    public virtual string EntityId { get;  set; }

    public string UserAgent { get; set; }

    public string ClientIpAddress { get; set; }

    /// <summary>
    /// Duration the length of seconds a user browsing
    /// </summary>
    public int Duration { get; set; }

    public virtual Guid? CreatorId { get; set; }

    public virtual DateTime CreationTime { get; set; }
}
