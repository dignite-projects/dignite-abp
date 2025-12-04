using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.UserPoints;
public class GetUserPointListInput: PagedResultRequestDto
{
    [CanBeNull]
    public virtual string PointType { get; set; }=null;

    [CanBeNull]
    public virtual string EntityType { get; set; } = null;

    [CanBeNull]
    public virtual string EntityId { get; set; } = null;
}
