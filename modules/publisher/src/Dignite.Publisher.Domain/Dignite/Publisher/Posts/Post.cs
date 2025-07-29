using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.Publisher.Posts;

/// <summary>
/// Represents a post in the publishing system.
/// </summary>
public abstract class Post: FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    protected Post()
    {
    }

    protected Post(Guid id, string? local, string title, string slug, string? coverImageUrl, string? summary,  DateTime? publishedTime, IEnumerable<Guid> categoryIds, Guid? tenantId)
        :base(id)
    {
        SetLocal(local);
        SetTitle(title);
        SetSlug(slug);
        SetCoverImageUrl(coverImageUrl);
        SetSummary(summary);
        PublishedTime = publishedTime;
        TenantId = tenantId;

        if (categoryIds != null)
        {
            foreach (var categoryId in categoryIds)
            {
                AddCategory(categoryId);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract string PostType { get; }

    /// <summary>
    /// The local identifier for the category.
    /// </summary>
    public virtual string? Local { get; protected set; }

    /// <summary>
    /// The title of the post
    /// </summary>
    public virtual string Title { get; protected set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual string Slug { get; protected set; }

    /// <summary>
    /// The URL of the cover image for the post.
    /// </summary>
    public virtual string? CoverImageUrl { get; protected set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual string? Summary { get; protected set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual PostStatus Status { get; protected set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual DateTime? PublishedTime { get; set; }

    /// <summary>
    /// The identifier of the tenant to which this category belongs.
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }

    public virtual int ViewCount { get; protected set; }

    public virtual ICollection<PostCategory> PostCategories { get; protected set; } = new List<PostCategory>();



    public virtual void IncreaseViewCount()
    {
        ViewCount++;
    }
    public virtual void SetTitle([NotNull] string title)
    {
        Title = Check.NotNullOrWhiteSpace(title, nameof(title), PostConsts.MaxTitleLength, 1);
    }

    public virtual void SetSlug([NotNull] string slug)
    {
        Slug = Check.NotNullOrWhiteSpace(slug, nameof(slug), PostConsts.MaxSlugLength, PostConsts.MinSlugLength);
    }
    public virtual void SetCoverImageUrl([NotNull] string coverImageUrl)
    {
        CoverImageUrl = Check.Length(coverImageUrl, nameof(coverImageUrl), PostConsts.MaxCoverImageUrlLength);
    }
    public virtual void SetSummary([NotNull] string summary)
    {
        Summary = Check.Length(summary, nameof(summary), PostConsts.MaxSummaryLength);
    }

    public virtual void SetLocal([NotNull] string local)
    {
        Local = Check.Length(local, nameof(local), PostConsts.MaxLocalLength);
    }

    public virtual void SetDraft()
    {
        Status = PostStatus.Draft;
    }

    public virtual void SetPublished()
    {
        Status = PostStatus.Published;
    }

    public virtual void SetPendingReview()
    {
        Status = PostStatus.PendingReview;
    }

    public virtual void SetArchived()
    {
        Status = PostStatus.Archived;
    }

    public virtual void AddCategory(Guid categoryId)
    {
        if (!PostCategories.Any(pc => pc.CategoryId == categoryId))
        {
            PostCategories.Add(new PostCategory(Id, categoryId, TenantId));
        }
    }

    public virtual void RemoveCategory(Guid categoryId)
    {
        PostCategories.RemoveAll(t => t.CategoryId == categoryId);
    }

    protected virtual void Update(string? local, string title, string slug, string? coverImageUrl, string? summary, DateTime? publishedTime, IEnumerable<Guid> categoryIds)
    {
        SetLocal(local);
        SetTitle(title);
        SetSlug(slug);
        SetCoverImageUrl(coverImageUrl);
        SetSummary(summary);
        PublishedTime = publishedTime;

        if (categoryIds == null || !categoryIds.Any())
        {
            foreach (var postCategory in PostCategories)
            {
                RemoveCategory(postCategory.CategoryId);
            }
        }
        else
        {
            foreach(var categoryId in PostCategories.Select(pc=>pc.CategoryId).Except(categoryIds).ToArray())
            {
                RemoveCategory(categoryId);
            }

            foreach (var categoryId in categoryIds.Except(PostCategories.Select(pc => pc.CategoryId)))
            {
                AddCategory(categoryId);
            }
        }
    }
}
