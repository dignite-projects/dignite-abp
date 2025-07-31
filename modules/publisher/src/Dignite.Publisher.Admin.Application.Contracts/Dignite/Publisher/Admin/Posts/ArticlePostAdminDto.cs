namespace Dignite.Publisher.Admin.Posts;
public class ArticlePostAdminDto : PostAdminDtoBase
{
    /// <summary>
    /// The content of the article post, typically in Markdown or HTML format.
    /// </summary>
    public string Content { get; set; }
}
