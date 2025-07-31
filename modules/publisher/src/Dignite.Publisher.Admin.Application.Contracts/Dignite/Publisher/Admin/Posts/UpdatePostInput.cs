using System;
using Volo.Abp.Domain.Entities;

namespace Dignite.Publisher.Admin.Posts;

[Serializable]
public abstract class UpdatePostInput : CreateOrUpdatePostInputBase, IHasConcurrencyStamp
{
    protected UpdatePostInput(string postType) : base(postType)
    {
    }

    public string ConcurrencyStamp { get; set; }
}
