using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace Dignite.Publisher.Categories;

[Serializable]
public class CategoryNotFoundException : BusinessException
{
    public CategoryNotFoundException([NotNull] Guid categoryId)
    {
        CategoryId = categoryId;
        Code = PublisherErrorCodes.Categories.NotFound;
        WithData(nameof(Category.Id), categoryId);
    }

    public virtual Guid CategoryId { get; }
}
