using System;
using System.Collections.Generic;
using System.Text;

namespace Dignite.Publisher.Posts;

/// <summary>
/// Represents the status of a post in the publishing system.
/// </summary>
public enum PostStatus
{
    /// <summary>
    /// The post is in draft status, meaning it is not yet published and can be edited.
    /// </summary>
    Draft,

    /// <summary>
    /// The post is pending review by the chief editor before publication.
    /// </summary>
    PendingReview,

    /// <summary>
    /// The post is published and visible to the public.
    /// </summary>
    Published,

    /// <summary>
    /// The post is archived, meaning it is hidden from public view but retained in the system for record-keeping, auditing, or future restoration.
    /// </summary>
    Archived
}
