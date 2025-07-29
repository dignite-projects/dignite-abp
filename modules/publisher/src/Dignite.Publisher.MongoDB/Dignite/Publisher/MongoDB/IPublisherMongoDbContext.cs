using Dignite.Publisher.Categories;
using Dignite.Publisher.Posts;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Publisher.MongoDB;

[ConnectionStringName(PublisherDbProperties.ConnectionStringName)]
public interface IPublisherMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<Category> Categories { get; }
    IMongoCollection<Post> Posts { get; }
    IMongoCollection<PostCategory> PostCategories { get; }
}
