using System;

namespace Dignite.Publisher.Admin.Posts;

[Serializable]
public abstract class UpdatePostInput : CreateOrUpdatePostInputBase
{
    protected UpdatePostInput(string postType) : base(postType)
    {
    }

    //public string ConcurrencyStamp { get; set; }
}
