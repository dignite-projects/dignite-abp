using System;
using System.ComponentModel.DataAnnotations;
using Dignite.Publisher.Posts;
using Volo.Abp.Validation;

namespace Dignite.Publisher.Admin.Posts;

[Serializable]
public class CreateVideoPostDto : CreatePostDto
{
    public CreateVideoPostDto():base(PostTypeConsts.VideoPostTypeName)
    {
    }

    /// <summary>
    /// <summary>
    /// The URL of the video file or stream.
    /// </summary>
    [Required]
    [DynamicMaxLength(typeof(VideoPostConsts), nameof(VideoPostConsts.MaxVideoUrlLength))]
    public string VideoUrl { get; set; }

    /// <summary>
    /// The duration of the video post, typically represented as a TimeSpan.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// An optional description of the video post, providing additional context or information.
    /// </summary>
    [DynamicMaxLength(typeof(VideoPostConsts), nameof(VideoPostConsts.MaxDescriptionLength))]
    public string? Description { get; set; }
}
