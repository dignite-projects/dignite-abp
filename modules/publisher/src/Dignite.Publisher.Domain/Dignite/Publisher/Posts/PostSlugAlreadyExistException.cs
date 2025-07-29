using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace Dignite.Publisher.Posts;

[Serializable]
public class PostSlugAlreadyExistException : BusinessException
{
    public PostSlugAlreadyExistException([NotNull] string? local, [NotNull] string slug)
    {
        Code = PublisherErrorCodes.Posts.SlugAlreadyExist;
        if (!string.IsNullOrWhiteSpace(local))
        {
            WithData(nameof(Post.Local), local);
        }
        WithData(nameof(Post.Slug), slug);
    }
}
