using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.UserPoints;

[Serializable]
public class UserPointDto: ExtensibleCreationAuditedEntityDto<Guid>
{
    /// <summary>
    /// Gets the name of the point type associated with this instance.
    /// </summary>
    public string? PointType { get; set; }

    /// <summary>
    /// Gets the amount associated with the current instance.
    /// </summary>
    public virtual int Amount { get; set; }

    /// <summary>
    /// Getting or setting the expiration time of point
    /// </summary>
    public virtual DateTime? ExpirationTime { get; set; }

    /// <summary>
    /// Gets the type name of the entity associated with this instance.
    /// </summary>
    public virtual string EntityType { get; set; }

    /// <summary>
    /// Gets the unique identifier for the entity.
    /// </summary>
    public virtual string EntityId { get; set; }

    /// <summary>
    /// Gets the description associated with the current instance.
    /// </summary>
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets the current balance.
    /// </summary>
    public virtual int Balance { get; set; }

    /// <summary>
    /// Gets the date and time when the next expiration is scheduled to occur, if available.
    /// </summary>
    /// <remarks>
    /// If a subsequent expiration time exists and is earlier than the current time, the user's points balance must be recalculated.
    /// </remarks>
    public DateTime? NextExpirationAt { get; set; }

    /// <summary>
    /// Gets or sets the of the primary key of the user associated with this point
    /// </summary>
    public virtual Guid UserId { get; set; }
}
