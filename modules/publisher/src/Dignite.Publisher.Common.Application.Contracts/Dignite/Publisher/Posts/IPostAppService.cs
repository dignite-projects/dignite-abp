using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Publisher.Posts;

public interface IPostAppService: IApplicationService
{
    Task<ListResultDto<PostDto>> GetListByIdsAsync(IEnumerable<Guid> ids);
}
