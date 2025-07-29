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

    public Post Create(CreatePostDto post, Guid postId, Guid? tenantId)
    {
        if (post is not CreateArticlePostDto articlePostDto)
        {
            throw new ArgumentException($"Invalid post type: {post.GetType().Name}. Expected: {nameof(CreateArticlePostDto)}");
        }

        return new ArticlePost(
            postId,
            post.Local,
            post.Title,
            post.Slug,
            post.CoverImageUrl,
            post.Summary,
            post.PublishedTime,
            post.CategoryIds,
            tenantId,
            articlePostDto.Content
            );
    }

    public void Update(Post post, UpdatePostDto input)
    {
        if (post is not ArticlePost articlePost)
        {
            throw new ArgumentException($"Invalid post type: {post.GetType().Name}. Expected: {nameof(ArticlePost)}");
        }
        if (input is not UpdateArticlePostDto updateArticlePostDto)
        {
            throw new ArgumentException($"Invalid input type: {input.GetType().Name}. Expected: {nameof(UpdateArticlePostDto)}");
        }

        articlePost.Update(
            input.Local,input.Title, input.Slug, input.CoverImageUrl, input.Summary, 
            input.PublishedTime,input.CategoryIds, updateArticlePostDto.Content);
    }
}
