using System;

namespace Dignite.Publisher.Posts;
public class VideoPostDto : PostDto
{
    /// <summary>
    /// The URL of the video file or stream.
    /// </summary>
    public virtual string VideoUrl { get; set; }

    /// <summary>
    /// The duration of the video post, typically represented as a TimeSpan.
    /// </summary>
    public virtual TimeSpan Duration { get; set; }

    /// <summary>
    /// An optional description of the video post, providing additional context or information.
    /// </summary>
    public virtual string? Description { get; set; }
}
