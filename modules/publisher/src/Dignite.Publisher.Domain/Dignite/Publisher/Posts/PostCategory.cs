using System;
using Dignite.Publisher.Categories;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Dignite.Publisher.Posts;
public class PostCategory : Entity, IMultiTenant
{
    public PostCategory(Guid postId, Guid categoryId, Guid? tenantId)
    {
        PostId = postId;
        CategoryId = categoryId;
        TenantId = tenantId;
    }

    public Guid PostId { get; protected set; }
    public Post Post { get; protected set; }

    public Guid CategoryId { get; protected set; }
    public Category Category { get; protected set; }

    public Guid? TenantId { get; protected set; }

    public override object[] GetKeys()
    {
        return new object[] { CategoryId, PostId };
    }
}
