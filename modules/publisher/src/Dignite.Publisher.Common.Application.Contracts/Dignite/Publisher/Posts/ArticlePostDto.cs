namespace Dignite.Publisher.Posts;
public class ArticlePostDto : PostDto
{
    /// <summary>
    /// The content of the article post, typically in Markdown or HTML format.
    /// </summary>
    public virtual string Content { get; set; }
}
