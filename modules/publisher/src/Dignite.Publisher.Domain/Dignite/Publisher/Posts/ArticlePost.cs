using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Dignite.Publisher.Posts;
public class ArticlePost : Post
{
    protected ArticlePost() : base()
    {
    }

    public ArticlePost(
        Guid id, string? locale, string title, string slug, string? coverBlobName, string? summary,
        DateTime? publishedTime, IEnumerable<Guid> categoryIds, Guid? tenantId,
        string content)
        : base(id, locale, title, slug, coverBlobName, summary, publishedTime, categoryIds, tenantId)
    {
        SetContent(content);
    }

    /// <summary>
    /// The content of the article post, typically in Markdown or HTML format.
    /// </summary>
    public virtual string Content { get; protected set; }

    /// <summary>
    /// 
    /// </summary>
    public override string PostType => PostTypeConsts.ArticlePostTypeName;


    public virtual void SetContent([NotNull] string content)
    {
        Content = content;
    }
    public virtual void Update(string? locale, string title, string slug, string? coverBlobName, string? summary,  DateTime? publishedTime, IEnumerable<Guid> categoryIds, string content)        
    {
        base.Update(locale, title, slug, coverBlobName, summary, publishedTime, categoryIds);
        SetContent(content);
    }
}
