using System;
using System.Threading.Tasks;
using Dignite.Publisher.Posts;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.CmsKit.Public.Comments;

namespace Dignite.Publisher.Public.Posts;
public interface IPostPublicAppService: IReadOnlyAppService<PostDtoBase,Guid, GetPostsInput>
{

    Task<PostDtoBase> GetAsync([CanBeNull] string? locale, [NotNull] string postSlug);

    Task<PagedResultDto<CmsUserDto>> GetCreatorsHasPostsAsync(PagedAndSortedResultRequestDto input);
}
