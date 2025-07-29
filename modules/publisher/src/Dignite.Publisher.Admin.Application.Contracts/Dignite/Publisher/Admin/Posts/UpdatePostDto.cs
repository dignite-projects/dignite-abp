using System;
using Volo.Abp.Domain.Entities;

namespace Dignite.Publisher.Admin.Posts;

[Serializable]
public abstract class UpdatePostDto : CreateOrUpdatePostDtoBase,IHasConcurrencyStamp
{
    protected UpdatePostDto(string postType) : base(postType)
    {
    }

    public string ConcurrencyStamp { get; set; }
}
