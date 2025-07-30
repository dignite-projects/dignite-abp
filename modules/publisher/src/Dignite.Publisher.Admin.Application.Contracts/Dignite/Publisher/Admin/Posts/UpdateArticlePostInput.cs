using System;
using Dignite.Publisher.Posts;

namespace Dignite.Publisher.Admin.Posts;

[Serializable]
public class UpdateArticlePostInput : UpdatePostInput
{
    public UpdateArticlePostInput() : base(PostTypeConsts.ArticlePostTypeName)
    {
    }

    /// <summary>
    /// The content of the article post, typically in Markdown or HTML format.
    /// </summary>
    public string? Content { get; set; }
}
