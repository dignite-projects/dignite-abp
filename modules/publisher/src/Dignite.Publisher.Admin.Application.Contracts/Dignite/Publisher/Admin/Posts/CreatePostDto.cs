using System;

namespace Dignite.Publisher.Admin.Posts;

[Serializable]
public abstract class CreatePostDto : CreateOrUpdatePostDtoBase
{
    protected CreatePostDto(string postType) : base(postType)
    {
    }
}
