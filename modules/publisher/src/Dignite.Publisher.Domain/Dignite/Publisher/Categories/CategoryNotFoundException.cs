using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace Dignite.Publisher.Categories;

[Serializable]
public class CategoryNotFoundException : BusinessException
{
    public CategoryNotFoundException([NotNull] Guid? categoryId)
    {
        Code = PublisherErrorCodes.Categories.NotFound;
        WithData(nameof(Category.Id), categoryId);
    }
}
