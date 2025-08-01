using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp;

namespace Dignite.Publisher.Posts;
public class VideoPost : Post
{
    protected VideoPost():base()
    {
    }

    public VideoPost(Guid id, string? local, string title, string slug, string? coverBlobName, string? summary, 
        DateTime? publishedTime, IEnumerable<Guid> categoryIds, Guid? tenantId,
        string videoUrl, TimeSpan duration, string? description) 
        : base(id, local, title, slug, coverBlobName, summary, publishedTime, categoryIds, tenantId)
    {
        SetVideoUrl(videoUrl);
        Duration = duration;
        Description = description;
    }

    /// <summary>
    /// The URL of the video file or stream.
    /// </summary>
    public virtual string VideoUrl { get; protected set; }

    /// <summary>
    /// The duration of the video post, typically represented as a TimeSpan.
    /// </summary>
    public virtual TimeSpan Duration { get; set; }

    /// <summary>
    /// An optional description of the video post, providing additional context or information.
    /// </summary>
    public virtual string? Description { get; protected set; }

    /// <summary>
    /// 
    /// </summary>
    public override string PostType => PostTypeConsts.VideoPostTypeName;

    public virtual void SetVideoUrl([NotNull] string videoUrl)
    {
        VideoUrl = Check.NotNullOrWhiteSpace(videoUrl, nameof(videoUrl), VideoPostConsts.MaxVideoUrlLength, 8);
    }

    public virtual void SetDescription([NotNull] string description)
    {
        Description = Check.NotNullOrWhiteSpace(description, nameof(description), VideoPostConsts.MaxDescriptionLength);
    }
    public virtual void Update(string? local, string title, string slug, string? coverBlobName, string? summary, DateTime? publishedTime, IEnumerable<Guid> categoryIds, string videoUrl, TimeSpan duration, string? description)
    {
        base.Update(local, title, slug, coverBlobName, summary, publishedTime, categoryIds);
        SetVideoUrl(videoUrl);
        Duration = duration;
        SetDescription(description);
    }
}
