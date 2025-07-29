using System;
using Dignite.Publisher.Posts;

namespace Dignite.Publisher.Admin.Posts;

[Serializable]
public class CreateArticlePostDto : CreatePostDto
{
    public CreateArticlePostDto() : base(PostTypeConsts.ArticlePostTypeName)
    {
    }

    /// <summary>
    /// The content of the article post, typically in Markdown or HTML format.
    /// </summary>
    public string? Content { get; set; }

}
