using System;
using System.Collections.Generic;
using Dignite.Publisher.Posts;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Dignite.Publisher.Public.Posts;
public class GetPostsInput: PagedResultRequestDto
{
    public GetPostsInput()
    {
        MaxResultCount = 20;
    }

    [DynamicMaxLength(typeof(PostConsts), nameof(PostConsts.MaxLocalLength))]
    public string? Local { get; set; }

    public IEnumerable<Guid>? CategoryIds { get; set; }

    public PostStatus? Status { get; set; }

    public string? PostType { get; set; }

    public Guid? CreatorId { get; set; }

    public DateTime? CreationTimeFrom { get; set; }

    public DateTime? CreationTimeTo { get; set; }
}
