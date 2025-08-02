using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace Dignite.Publisher.Posts;

[Serializable]
public class PostSlugAlreadyExistException : BusinessException
{
    public PostSlugAlreadyExistException([NotNull] string? locale, [NotNull] string slug)
    {
        Locale = locale;
        Slug = slug;
        Code = PublisherErrorCodes.Posts.SlugAlreadyExist;
        if (!string.IsNullOrWhiteSpace(locale))
        {
            WithData(nameof(Post.Locale), locale);
        }
        WithData(nameof(Post.Slug), slug);
    }

    public virtual string? Locale { get; }
    public virtual string Slug { get; }
}
