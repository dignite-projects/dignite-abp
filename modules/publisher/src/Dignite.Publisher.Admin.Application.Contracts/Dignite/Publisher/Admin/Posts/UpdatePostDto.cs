using System;

namespace Dignite.Publisher.Admin.Posts;

[Serializable]
public abstract class UpdatePostDto : CreateOrUpdatePostDtoBase
{
    protected UpdatePostDto(string postType) : base(postType)
    {
    }

    //public string ConcurrencyStamp { get; set; }
}
