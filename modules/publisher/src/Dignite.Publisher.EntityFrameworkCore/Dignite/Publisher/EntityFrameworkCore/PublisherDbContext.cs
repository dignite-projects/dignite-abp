using Dignite.Publisher.Categories;
using Dignite.Publisher.Posts;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Publisher.EntityFrameworkCore;

[ConnectionStringName(PublisherDbProperties.ConnectionStringName)]
public class PublisherDbContext : AbpDbContext<PublisherDbContext>, IPublisherDbContext
{
    public DbSet<Category> Categories { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<PostCategory> PostCategories { get; set; }

    public PublisherDbContext(DbContextOptions<PublisherDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigurePublisher();
    }
}
