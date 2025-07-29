using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Dignite.Publisher.Categories;

[Serializable]
public class CategoryDto : ExtensibleEntityDto<Guid>
{
    /// <summary>
    /// The display name of the category.
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// The name of the category, which is typically used as a unique identifier.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The description of the category, providing additional context or information.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The local identifier for the category.
    /// </summary>
    public string? Local { get; set; }

    /// <summary>
    /// Sorting value, the smaller the value, the higher the ranking
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Whether it is active (for scenarios where it is no longer used but the data is retained)
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Parent category ID (supports multi-level category structure)
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// A list of post type names that this category is applicable to.
    /// Example: ["ArticlePost", "VideoPost"]
    /// </summary>
    public List<string> PostTypes { get; set; }

    public List<CategoryDto> Children { get; set; }
}
