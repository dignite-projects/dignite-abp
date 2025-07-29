using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dignite.Publisher.Posts;
using Volo.Abp.Application.Dtos;

namespace Dignite.Publisher.Posts;

public class PostAppService : PublisherAppServiceBase, IPostAppService
{
    protected readonly IPostRepository PostRepository;

    public PostAppService(IPostRepository postRepository)
    {
        PostRepository = postRepository;
    }

    public async Task<ListResultDto<PostDto>> GetListByIdsAsync(IEnumerable<Guid> ids)
    {
        var list = await PostRepository.GetListByIdsAsync(ids);
        

        return new ListResultDto<PostDto>(ObjectMapper.Map<List<Post>, List<PostDto>>(list));
    }
}