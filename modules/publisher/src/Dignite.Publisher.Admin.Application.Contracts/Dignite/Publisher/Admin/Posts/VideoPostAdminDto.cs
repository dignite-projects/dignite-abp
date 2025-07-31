using System;

namespace Dignite.Publisher.Admin.Posts;
public class VideoPostAdminDto : PostAdminDtoBase
{
    /// <summary>
    /// The URL of the video file or stream.
    /// </summary>
    public string VideoUrl { get; set; }
    /// <summary>
    /// The duration of the video post, typically represented as a TimeSpan.
    /// </summary>
    public TimeSpan Duration { get; set; }
    /// <summary>
    /// An optional description of the video post, providing additional context or information.
    /// </summary>
    public string? Description { get; set; }
}