using System;
using Dignite.Publisher.Features;
using Dignite.Publisher.GlobalFeatures;
using Dignite.Publisher.Posts;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;

namespace Dignite.Publisher.Admin.Posts;

/// <summary>
/// Implements the IPostBuilder interface for creating and updating VideoPost entities. 
/// </summary>
[RequiresGlobalFeature(typeof(VideoPostsFeature))]
[RequiresFeature(PublisherFeatures.VideoPostEnable)]
public class VideoPostBuilder : IPostBuilder
{
    public string PostTypeName => PostTypeConsts.VideoPostTypeName;

    public Post Create(CreatePostInput post, Guid postId, Guid? tenantId)
    {
        if (post is not CreateVideoPostInput videoPostDto)
        {
            throw new ArgumentException($"Invalid post type: {post.GetType().Name}. Expected: {nameof(CreateVideoPostInput)}");
        }

        return new VideoPost(
            postId,
            post.Local,
            post.Title,
            post.Slug,
            post.CoverImageUrl,
            post.Summary,
            post.PublishedTime,
            post.CategoryIds,
            tenantId,
            videoPostDto.VideoUrl,
            videoPostDto.Duration,
            videoPostDto.Description
            );
    }

    public void Update(Post post, UpdatePostInput input)
    {
        if (post is not VideoPost videoPost)
        {
            throw new ArgumentException($"Invalid post type: {post.GetType().Name}. Expected: {nameof(VideoPost)}");
        }
        if (input is not UpdateVideoPostInput updateVideoPostDto)
        {
            throw new ArgumentException($"Invalid input type: {input.GetType().Name}. Expected: {nameof(UpdateVideoPostInput)}");
        }


        videoPost.Update(
            input.Local, input.Title, input.Slug, input.CoverImageUrl, input.Summary,
            input.PublishedTime, input.CategoryIds, 
            updateVideoPostDto.VideoUrl, updateVideoPostDto.Duration, updateVideoPostDto.Description);
    }
}
