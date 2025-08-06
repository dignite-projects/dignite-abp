using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Publisher.Admin.Permissions;
using Dignite.Publisher.Categories;
using Dignite.Publisher.Posts;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;

namespace Dignite.Publisher.Admin.Posts;

[Authorize(PublisherAdminPermissions.Posts.Default)]
public class PostAdminAppService : PublisherAdminAppService, IPostAdminAppService
{
    protected CategoryManager CategoryManager { get; }
    protected IPostRepository PostRepository { get; }
    protected IPostBuilderSelector PostBuilderSelector { get; }
    protected PostManager PostManager { get; }

    public PostAdminAppService(CategoryManager categoryManager, IPostRepository postRepository, IPostBuilderSelector postBuilderSelector, PostManager postManager)
    {
        CategoryManager = categoryManager;
        PostRepository = postRepository;
        PostBuilderSelector = postBuilderSelector;
        PostManager = postManager;
    }

    [Authorize(PublisherAdminPermissions.Posts.Create)]
    public async Task<PostAdminDtoBase> CreateAsync(CreatePostInput input)
    {
        await PostManager.CheckSlugExistenceAsync(input.Locale, input.Slug);
        if (input.CategoryIds.Any())
        {
            await CategoryManager.CheckExistenceAsync(input.Locale, input.CategoryIds);
        }

        var postBuilder = PostBuilderSelector.Get(input.PostType);
        var post = postBuilder.Create(input, GuidGenerator.Create(), CurrentTenant.Id);
        input.MapExtraPropertiesTo(post);
        await PostRepository.InsertAsync(post);

        return ObjectMapper.Map<Post, PostAdminDtoBase>(post);
    }

    public async Task DeleteAsync(Guid id)
    {
        var post = await PostRepository.GetAsync(id,false);
        await AuthorizationService.CheckAsync(post, CommonOperations.Delete);
        await PostManager.DeleteAsync(post);
    }

    public async Task<PostAdminDtoBase> GetAsync(Guid id)
    {
        var post = await PostRepository.GetAsync(id);
        return ObjectMapper.Map<Post, PostAdminDtoBase>(post);
    }

    public async Task<PagedResultDto<PostAdminDtoBase>> GetListAsync(GetPostsInput input)
    {
        var dto = new List<PostAdminDtoBase>();
        var count = await PostRepository.GetCountAsync(input.Locale, input.CategoryIds, input.Status, input.PostType, input.CreatorId,
            creationTimeFrom: input.CreationTimeFrom,
            creationTimeTo: input.CreationTimeTo);
        if (count == 0)
        {
            return new PagedResultDto<PostAdminDtoBase>(count, dto);
        }
        var list = await PostRepository.GetPagedListAsync(
            input.Locale, input.CategoryIds, input.Status, input.PostType, input.CreatorId,
            creationTimeFrom: input.CreationTimeFrom,
            creationTimeTo: input.CreationTimeTo,
            skipCount: input.SkipCount,
            maxResultCount: input.MaxResultCount,
            sorting: input.Sorting
        );

        dto = ObjectMapper.Map<List<Post>, List<PostAdminDtoBase>>(list);

        return new PagedResultDto<PostAdminDtoBase>(count, dto);
    }

    public async Task<bool> SlugExistsAsync(string? locale, string slug)
    {
        return await PostRepository.SlugExistsAsync(locale, slug);
    }

    public async Task<PostAdminDtoBase> UpdateAsync(Guid id, UpdatePostInput input)
    {
        var post = await PostRepository.GetAsync(id);
        await AuthorizationService.CheckAsync(post, CommonOperations.Update);
        if (post.Locale != input.Locale || post.Slug != input.Slug)
        {
            await PostManager.CheckSlugExistenceAsync(input.Locale, input.Slug);
        }

        if (input.CategoryIds.Any())
        {
            await CategoryManager.CheckExistenceAsync(input.Locale, input.CategoryIds);
        }

        // Set the concurrency stamp if it is not null to ensure optimistic concurrency control
        post.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

        // Get the appropriate post builder based on the post type
        var postBuilder = PostBuilderSelector.Get(post.PostType);
        postBuilder.Update(post, input);
        input.MapExtraPropertiesTo(post);
        await PostRepository.UpdateAsync(post);
        return ObjectMapper.Map<Post, PostAdminDtoBase>(post);
    }

    public async Task DraftAsync(Guid id)
    {
        var post = await PostRepository.GetAsync(id);
        await AuthorizationService.CheckAsync(post, CommonOperations.Update);
        post.SetDraft();
        await PostRepository.UpdateAsync(post);
    }

    public async Task SendToReviewAsync(Guid id)
    {
        var post = await PostRepository.GetAsync(id);
        await AuthorizationService.CheckAsync(post, CommonOperations.Update);
        post.SetPendingReview();
        await PostRepository.UpdateAsync(post);
    }

    [Authorize(PublisherAdminPermissions.Posts.Publish)]
    public async Task PublishAsync(Guid id)
    {
        var post = await PostRepository.GetAsync(id);
        post.SetPublished();
        await PostRepository.UpdateAsync(post);
    }

    [Authorize(PublisherAdminPermissions.Posts.Publish)]
    public async Task<bool> HasPostPendingForReviewAsync()
    {
        return await PostRepository.HasPostPendingForReviewAsync();
    }

    public async Task ArchiveAsync(Guid id)
    {
        var post = await PostRepository.GetAsync(id);
        await AuthorizationService.CheckAsync(post, CommonOperations.Update);
        post.SetArchived();
        await PostRepository.UpdateAsync(post);
    }
}
