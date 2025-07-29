using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dignite.Publisher.Categories;
using Volo.Abp.Validation;

namespace Dignite.Publisher.Admin.Categories;
public abstract class CreateOrUpdateCategoryDtoBase
{
    /// <summary>
    /// The display name of the category.
    /// </summary>
    [Required]
    [DynamicMaxLength(typeof(CategoryConsts), nameof(CategoryConsts.MaxDisplayNameLength))]
    public string DisplayName { get; set; }

    /// <summary>
    /// The name of the category, which is typically used as a unique identifier.
    /// </summary>        
    [Required]
    [DynamicMaxLength(typeof(CategoryConsts), nameof(CategoryConsts.MaxNameLength))]
    [RegularExpression(CategoryConsts.NameRegularExpression)]
    public string Name { get; set; }

    /// <summary>
    /// The description of the category, providing additional context or information.
    /// </summary>
    [DynamicMaxLength(typeof(CategoryConsts), nameof(CategoryConsts.MaxDescriptionLength))]
    public string? Description { get; set; }

    /// <summary>
    /// Whether it is active (for scenarios where it is no longer used but the data is retained)
    /// </summary>
    [Required]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// A list of post type names that this category is applicable to.
    /// Example: ["ArticlePost", "VideoPost"]
    /// </summary>
    public List<string> PostTypes { get; set; } = new();
}
