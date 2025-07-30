using System;
using System.Collections.Generic;
using System.Text;
using Dignite.Publisher.Posts;
using Volo.Abp.Application.Services;

namespace Dignite.Publisher.Public.Posts;
public interface IPostPublicAppService: IReadOnlyAppService<PostDto,Guid, GetPostsInput>
{
}
