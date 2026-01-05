using System;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.UserPoints;
public class GetUserPointTransactionsInput: PagedResultRequestDto
{
    [CanBeNull]
    public virtual Guid? AccountId { get; set; } = null;

    [CanBeNull]
    public virtual string EntityType { get; set; } = null;

    [CanBeNull]
    public virtual string EntityId { get; set; } = null;
}
