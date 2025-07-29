using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace Dignite.Publisher.Posts;

[Serializable]
public class PostSlugAlreadyExistException : BusinessException
{
    public PostSlugAlreadyExistException([NotNull] string? local, [NotNull] string slug)
    {
        Local = local;
        Slug = slug;
        Code = PublisherErrorCodes.Posts.SlugAlreadyExist;
        if (!string.IsNullOrWhiteSpace(local))
        {
            WithData(nameof(Post.Local), local);
        }
        WithData(nameof(Post.Slug), slug);
    }

    public virtual string? Local { get; }
    public virtual string Slug { get; }
}
