using System;

namespace Dignite.Publisher.Admin.Posts;

[Serializable]
public abstract class CreatePostInput : CreateOrUpdatePostInputBase
{
    protected CreatePostInput(string postType) : base(postType)
    {
    }
}
