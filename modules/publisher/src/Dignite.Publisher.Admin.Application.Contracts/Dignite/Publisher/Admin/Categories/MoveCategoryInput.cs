using System;

namespace Dignite.Publisher.Admin.Categories;
public class MoveCategoryInput
{
    /// <summary>
    /// Parent category ID (supports multi-level category structure)
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// Sorting value, the smaller the value, the higher the ranking
    /// </summary>
    public int Order { get; set; }
}
