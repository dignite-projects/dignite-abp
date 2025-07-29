using Dignite.Publisher.Categories;
using Dignite.Publisher.Posts;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Publisher.MongoDB;

[ConnectionStringName(PublisherDbProperties.ConnectionStringName)]
public class PublisherMongoDbContext : AbpMongoDbContext, IPublisherMongoDbContext
{
    public IMongoCollection<Category> Categories => Collection<Category>();

    public IMongoCollection<Post> Posts => Collection<Post>();

    public IMongoCollection<PostCategory> PostCategories => Collection<PostCategory>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigurePublisher();
    }
}
