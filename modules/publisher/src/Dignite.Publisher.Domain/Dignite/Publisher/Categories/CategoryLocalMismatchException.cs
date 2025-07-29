using Volo.Abp;

namespace Dignite.Publisher.Categories;
public class CategoryLocalMismatchException : BusinessException
{
    public CategoryLocalMismatchException()
    {
        Code = PublisherErrorCodes.Categories.LocalMismatch;
    }
}
