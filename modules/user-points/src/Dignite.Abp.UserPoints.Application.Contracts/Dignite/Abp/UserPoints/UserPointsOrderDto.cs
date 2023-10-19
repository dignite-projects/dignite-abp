using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.UserPoints;

[Serializable]
public class UserPointsOrderDto : ExtensibleCreationAuditedEntityDto<Guid>
{
    /// <summary>
    /// Getting or Setting Points
    /// </summary>
    public virtual int Points { get; protected set; }

    /// <summary>
    /// Get or set the business order type associated with a points order
    /// </summary>
    public virtual string BusinessOrderType { get; protected set; }

    /// <summary>
    /// Get or set the business order number associated with a points order
    /// </summary>
    public virtual string BusinessOrderNumber { get; protected set; }

    /// <summary>
    /// Gets or sets the of the primary key of the user associated with this order.
    /// </summary>
    public virtual Guid UserId { get; protected set; }
}
