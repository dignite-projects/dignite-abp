using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dignite.Publisher.Posts;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;
using Volo.CmsKit.Users;
using CmsUserDto = Volo.CmsKit.Public.Comments.CmsUserDto;

namespace Dignite.Publisher.Public.Posts;
public class PostPublicAppService : PublisherPublicAppService, IPostPublicAppService
{
    protected IPostRepository PostRepository;

    public async Task<PostDtoBase> GetAsync([CanBeNull] string? locale, [NotNull] string postSlug)
    {
        var post = await PostRepository.GetBySlugAsync(locale, postSlug);
        return ObjectMapper.Map<Post, PostDtoBase>(post);
    }

    public async Task<PostDtoBase> GetAsync(Guid id)
    {
        var post = await PostRepository.GetAsync(id);
        return ObjectMapper.Map<Post, PostDtoBase>(post);
    }

    public async Task<PagedResultDto<CmsUserDto>> GetCreatorsHasPostsAsync(PagedAndSortedResultRequestDto input)
    {
        var count = await PostRepository.GetCreatorsHasPostsCountAsync();
        var list = await PostRepository.GetCreatorsHasPostsAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
        var dtos = ObjectMapper.Map<List<CmsUser>, List<CmsUserDto>>(list);
        return new PagedResultDto<CmsUserDto>(count, dtos);
    }

    public async Task<PagedResultDto<PostDtoBase>> GetListAsync(GetPostsInput input)
    {
        Guid? favoriteUserId = await GetFavoriteUserIdAsync(input.FilterOnFavorites);
        var count = await PostRepository.GetCountAsync(
            input.Locale,
            input.CategoryIds,
            PostStatus.Published,
            input.PostType,
            input.CreatorId,
            input.TagId,
            favoriteUserId,
            input.CreationTimeFrom,
            input.CreationTimeTo
        );
        var list = await PostRepository.GetPagedListAsync(
            input.Locale,
            input.CategoryIds,
            PostStatus.Published,
            input.PostType,
            input.CreatorId,
            input.TagId,
            favoriteUserId,
            input.CreationTimeFrom,
            input.CreationTimeTo,
            input.SkipCount, input.MaxResultCount);

        var dtos = ObjectMapper.Map<List<Post>, List<PostDtoBase>>(list);
        return new PagedResultDto<PostDtoBase>(count, dtos);
    }

    protected virtual async Task<Guid?> GetFavoriteUserIdAsync(bool? filterOnFavorites)
    {
        if (!filterOnFavorites.GetValueOrDefault() || !CurrentUser.IsAuthenticated)
        {
            return null;
        }

        return CurrentUser.GetId();
    }
}
