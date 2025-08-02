using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.CmsKit.Users;

namespace Dignite.Publisher.Posts;

/// <summary>
/// Represents a post in the publishing system.
/// </summary>
public abstract class Post: FullAuditedAggregateRoot<Guid>, IMultiTenant, IHasEntityVersion
{
    protected Post()
    {
    }

    protected Post(Guid id, string? locale, string title, string slug, string? coverBlobName, string? summary,  DateTime? publishedTime, IEnumerable<Guid> categoryIds, Guid? tenantId)
        :base(id)
    {
        SetLocale(locale);
        SetTitle(title);
        SetSlug(slug);
        SetCoverBlobName(coverBlobName);
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
    /// The locale identifier for the category.
    /// </summary>
    public virtual string? Locale { get; protected set; }

    /// <summary>
    /// The title of the post
    /// </summary>
    public virtual string Title { get; protected set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual string Slug { get; protected set; }

    /// <summary>
    /// The Blob Name of the cover image for the post.
    /// </summary>
    public virtual string? CoverBlobName { get; protected set; }

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
    /// 
    /// </summary>
    public virtual CmsUser Creator { get; set; }

    /// <summary>
    /// The identifier of the tenant to which this category belongs.
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }

    public virtual int ReadCount { get; protected set; }

    public virtual int CommentCount { get; protected set; }

    public virtual int LikeCount { get; protected set; }

    public virtual int FavoriteCount { get; protected set; }

    public virtual ICollection<PostCategory> PostCategories { get; protected set; } = new List<PostCategory>();

    public virtual int EntityVersion { get; protected set; }

    public virtual void SetTitle([NotNull] string title)
    {
        Title = Check.NotNullOrWhiteSpace(title, nameof(title), PostConsts.MaxTitleLength, 1);
    }

    public virtual void SetSlug([NotNull] string slug)
    {
        Slug = Check.NotNullOrWhiteSpace(slug, nameof(slug), PostConsts.MaxSlugLength, PostConsts.MinSlugLength);
    }
    public virtual void SetCoverBlobName([NotNull] string coverBlobName)
    {
        CoverBlobName = Check.Length(coverBlobName, nameof(coverBlobName), PostConsts.MaxCoverBlobNameLength);
    }
    public virtual void SetSummary([NotNull] string summary)
    {
        Summary = Check.Length(summary, nameof(summary), PostConsts.MaxSummaryLength);
    }

    public virtual void SetLocale([NotNull] string locale)
    {
        Locale = Check.Length(locale, nameof(locale), PostConsts.MaxLocaleLength);
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

    public virtual void SetCategoies(IEnumerable<Guid> categoryIds)
    {
        if (categoryIds == null || !categoryIds.Any())
        {
            foreach (var postCategory in PostCategories)
            {
                RemoveCategory(postCategory.CategoryId);
            }
        }
        else
        {
            foreach (var categoryId in PostCategories.Select(pc => pc.CategoryId).Except(categoryIds).ToArray())
            {
                RemoveCategory(categoryId);
            }

            foreach (var categoryId in categoryIds.Except(PostCategories.Select(pc => pc.CategoryId)))
            {
                AddCategory(categoryId);
            }
        }
    }

    protected virtual void AddCategory(Guid categoryId)
    {
        if (!PostCategories.Any(pc => pc.CategoryId == categoryId))
        {
            PostCategories.Add(new PostCategory(Id, categoryId, TenantId));
        }
    }

    protected virtual void RemoveCategory(Guid categoryId)
    {
        PostCategories.RemoveAll(t => t.CategoryId == categoryId);
    }

    protected virtual void Update(string? locale, string title, string slug, string? coverBlobName, string? summary, DateTime? publishedTime, IEnumerable<Guid> categoryIds)
    {
        SetLocale(locale);
        SetTitle(title);
        SetSlug(slug);
        SetCoverBlobName(coverBlobName);
        SetSummary(summary);
        PublishedTime = publishedTime;
        SetCategoies(categoryIds);
    }

    public virtual void IncreaseReadCount(int count)
    {
        ReadCount += count;
    }
    public virtual void IncreaseCommentCount(int count)
    {
        CommentCount += count;
    }
    public virtual void IncreaseLikeCount(int count)
    {
        LikeCount += count;
    }
    public virtual void IncreaseFavoriteCount(int count)
    {
        FavoriteCount += count;
    }
}
