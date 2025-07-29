using Dignite.Publisher.Categories;
using Dignite.Publisher.Posts;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Publisher.EntityFrameworkCore;

[ConnectionStringName(PublisherDbProperties.ConnectionStringName)]
public interface IPublisherDbContext : IEfCoreDbContext
{
    DbSet<Category> Categories { get; }
    DbSet<Post> Posts { get; }
    DbSet<PostCategory> PostCategories { get; }
}
