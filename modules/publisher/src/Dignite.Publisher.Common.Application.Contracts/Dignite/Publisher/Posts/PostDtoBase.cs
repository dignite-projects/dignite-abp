using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;

namespace Dignite.Publisher.Posts;

[Serializable]
public abstract class PostDtoBase : ExtensibleEntityDto<Guid>, IMayHaveCreator, IHasCreationTime, IHasModificationTime
{
    /// <summary>
    /// 
    /// </summary>
    public string PostType { get; set; }

    /// <summary>
    /// The local identifier for the category.
    /// </summary>
    public string? Local { get; set; }

    /// <summary>
    /// The title of the post
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Slug { get; set; }

    /// <summary>
    /// The URL of the cover image for the post.
    /// </summary>
    public string? CoverImageUrl { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public PostStatus Status { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime? PublishedTime { get; set; }

    public virtual int ReadCount { get; set; }

    public virtual int CommentCount { get; set; }

    public virtual int LikeCount { get; set; }

    public virtual int FavoriteCount { get; set; }

    public ICollection<PostCategoryDto> PostCategories { get; set; } = new List<PostCategoryDto>();


    public Guid? CreatorId { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime? LastModificationTime { get; set; }
}
