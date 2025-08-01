using System;
using Dignite.Publisher.Features;
using Dignite.Publisher.GlobalFeatures;
using Dignite.Publisher.Posts;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;

namespace Dignite.Publisher.Admin.Posts;

/// <summary>
/// Implements the IPostBuilder interface for creating and updating ArticlePost entities. 
/// </summary>
[RequiresGlobalFeature(typeof(ArticlePostsFeature))]
[RequiresFeature(PublisherFeatures.ArticlePostEnable)]
public class ArticlePostBuilder : IPostBuilder
{
    public string PostTypeName => PostTypeConsts.ArticlePostTypeName;

    public Post Create(CreatePostInput input, Guid postId, Guid? tenantId)
    {
        if (input is not CreateArticlePostInput articlePostDto)
        {
            throw new ArgumentException($"Invalid post type: {input.GetType().Name}. Expected: {nameof(CreateArticlePostInput)}");
        }

        return new ArticlePost(
            postId,
            input.Local,
            input.Title,
            input.Slug,
            input.CoverBlobName,
            input.Summary,
            input.PublishedTime,
            input.CategoryIds,
            tenantId,
            articlePostDto.Content
            );
    }

    public void Update(Post post, UpdatePostInput input)
    {
        if (post is not ArticlePost articlePost)
        {
            throw new ArgumentException($"Invalid post type: {post.GetType().Name}. Expected: {nameof(ArticlePost)}");
        }
        if (input is not UpdateArticlePostInput updateArticlePostDto)
        {
            throw new ArgumentException($"Invalid input type: {input.GetType().Name}. Expected: {nameof(UpdateArticlePostInput)}");
        }

        articlePost.Update(
            input.Local,input.Title, input.Slug, input.CoverBlobName, input.Summary, 
            input.PublishedTime,input.CategoryIds, updateArticlePostDto.Content);
    }
}
