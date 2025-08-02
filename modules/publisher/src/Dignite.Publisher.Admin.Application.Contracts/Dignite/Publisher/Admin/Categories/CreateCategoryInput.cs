using System;
using Dignite.Publisher.Categories;
using Volo.Abp.Validation;

namespace Dignite.Publisher.Admin.Categories;
public class CreateCategoryInput : CreateOrUpdateCategoryInputBase
{
    /// <summary>
    /// The locale identifier for the category.
    /// </summary>
    [DynamicMaxLength(typeof(CategoryConsts), nameof(CategoryConsts.MaxLocaleLength))]
    public string? Locale { get; set; }

    /// <summary>
    /// Parent category ID (supports multi-level category structure)
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// Sorting value, the smaller the value, the higher the ranking
    /// </summary>
    public int Order { get; set; }
}
