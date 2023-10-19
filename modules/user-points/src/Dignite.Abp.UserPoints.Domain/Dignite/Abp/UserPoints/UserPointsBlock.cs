using System;
using Volo.Abp.Domain.Entities;

namespace Dignite.Abp.UserPoints;

/// <summary>
/// Decompose user points into minimum factor blocks and record them in the database table
/// </summary>
public class UserPointsBlock : Entity<Guid>
{
    protected UserPointsBlock()
    {
    }

    public UserPointsBlock(Guid userPointsItemId)
    {
        UserPointsItemId = userPointsItemId;
    }

    /// <summary>
    /// Get or set the <see cref="UserPointsItem.Id"/> associated with a point block
    /// </summary>
    public virtual Guid UserPointsItemId { get; protected set; }

    /// <summary>
    /// Get or set the <see cref="UserPointsItem"/> associated with a point block
    /// </summary>
    public virtual UserPointsItem UserPointsItem { get; protected set; }

    /*
    /// <summary>
    /// Getting or Setting Points Block.
    /// Each block value is equal to <see cref="DignitePointsBlockOptions.Factor"/>
    /// </summary>
    public virtual int Block { get; protected set; }
    */

    /// <summary>
    /// Get or set whether a block of points is locked
    /// </summary>
    public virtual bool IsLocked { get; protected set; }

    public virtual void SetLocked()
    {
        IsLocked = true;
    }
}
