using System;
using Dignite.Publisher.Categories;
using Volo.Abp.Validation;

namespace Dignite.Publisher.Admin.Categories;
public class CreateCategoryDto : CreateOrUpdateCategoryDtoBase
{
    /// <summary>
    /// The local identifier for the category.
    /// </summary>
    [DynamicMaxLength(typeof(CategoryConsts), nameof(CategoryConsts.MaxLocalLength))]
    public string? Local { get; set; }

    /// <summary>
    /// Parent category ID (supports multi-level category structure)
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// Sorting value, the smaller the value, the higher the ranking
    /// </summary>
    public int Order { get; set; }
}
