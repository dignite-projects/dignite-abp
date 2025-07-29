using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace Dignite.Publisher.Categories;

[Serializable]
public class CategoryNameAlreadyExistException : BusinessException
{
    public CategoryNameAlreadyExistException([NotNull] Guid? parentId, [NotNull] string name)
    {
        Code = PublisherErrorCodes.Categories.NameAlreadyExist;
        WithData(nameof(Category.ParentId), parentId);
        WithData(nameof(Category.Name), name);
    }
}
