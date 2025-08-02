using System;
using System.Collections.Generic;
using Dignite.Publisher.Posts;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Dignite.Publisher.Categories;

/// <summary>
/// A category of articles representing publishers
/// </summary>
public class Category : AggregateRoot<Guid>, IMultiTenant
{
    public Category(Guid id, string? locale, Guid? parentId, string displayName, string name, string? description, bool isActive, List<string> postTypes, int order, Guid? tenantId)
        :base(id)
    {
        Locale = locale;
        ParentId = parentId;
        DisplayName = displayName;
        Name = name;
        Description = description;
        IsActive = isActive;
        postTypes.ForEach(AddPostType);
        Order = order;
        TenantId = tenantId;
    }

    /// <summary>
    /// The locale identifier for the category.
    /// </summary>
    public virtual string? Locale { get; protected set; }

    /// <summary>
    /// Parent category ID (supports multi-level category structure)
    /// </summary>
    public virtual Guid? ParentId { get; set; }

    /// <summary>
    /// The display name of the category.
    /// </summary>
    public virtual string DisplayName { get; protected set; }

    /// <summary>
    /// The name of the category, which is typically used as a unique identifier.
    /// </summary>
    public virtual string Name { get; protected set; }

    /// <summary>
    /// The description of the category, providing additional context or information.
    /// </summary>
    public virtual string? Description { get; protected set; }

    /// <summary>
    /// Whether it is active (for scenarios where it is no longer used but the data is retained)
    /// </summary>
    public virtual bool IsActive { get; set; } = true;

    /// <summary>
    /// A list of post type names that this category is applicable to.
    /// Example: ["ArticlePost", "VideoPost"]
    /// </summary>
    public virtual List<string> PostTypes { get; protected set; } = new();

    /// <summary>
    /// Sorting value, the smaller the value, the higher the ranking
    /// </summary>
    public virtual int Order { get; set; }

    /// <summary>
    /// The identifier of the tenant to which this category belongs.
    /// </summary>
    public virtual Guid? TenantId { get; set; }

    /// <summary>
    /// Children are not configured in EF and can be used as auxiliary properties for front-end/DTO.
    /// </summary>
    public List<Category> Children { get; set; }

    public virtual ICollection<PostCategory> CategoryPosts { get; protected set; } = new List<PostCategory>();

    public virtual Category SetDisplayName([NotNull] string displayName)
    {
        DisplayName = Check.NotNullOrWhiteSpace(displayName, nameof(displayName), CategoryConsts.MaxDisplayNameLength, 1);
        return this;
    }

    public virtual Category SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), CategoryConsts.MaxNameLength, 1);
        return this;
    }

    public virtual Category SetDescription([NotNull] string description)
    {
        Description = Check.Length(description, nameof(description), CategoryConsts.MaxDescriptionLength);
        return this;
    }

    public virtual Category SetLocal([NotNull] string locale)
    {
        Locale = Check.Length(locale, nameof(locale), CategoryConsts.MaxLocaleLength);
        return this;
    }

    public virtual void AddPostType([NotNull] string postType)
    {
        PostTypes.AddIfNotContains(postType);
    }

    public virtual void RemovePostType([NotNull] string postType)
    {
        PostTypes.RemoveAll(t => t == postType);
    }
}
