using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace Dignite.Publisher.Categories;

[Serializable]
public class CategoryCannotMoveToChildException : BusinessException
{
    public CategoryCannotMoveToChildException()
    {
        Code = PublisherErrorCodes.Categories.NotFound;
    }
}
