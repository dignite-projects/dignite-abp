using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dignite.Publisher.Posts;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace Dignite.Publisher.Admin.Posts;

[Serializable]
public abstract class CreateOrUpdatePostInputBase: ExtensibleObject
{
    public CreateOrUpdatePostInputBase(string postType)
    {
        PostType = postType;
    }

    /// <summary>
    /// 
    /// </summary>
    public string PostType { get; protected set; }

    /// <summary>
    /// The locale identifier for the category.
    /// </summary>
    [DynamicMaxLength(typeof(PostConsts), nameof(PostConsts.MaxLocaleLength))]
    public string? Locale { get; set; }

    /// <summary>
    /// The title of the post
    /// </summary>
    [Required]
    [DynamicMaxLength(typeof(PostConsts), nameof(PostConsts.MaxTitleLength))]
    public string Title { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Required]
    [DynamicMaxLength(typeof(PostConsts), nameof(PostConsts.MaxSlugLength))]
    [RegularExpression(PostConsts.SlugRegularExpression)]
    public string Slug { get; set; }

    /// <summary>
    /// The URL of the cover image for the post.
    /// </summary>
    public string? CoverBlobName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime? PublishedTime { get; set; }

    public List<Guid> CategoryIds { get; set; } = new List<Guid>();
}
