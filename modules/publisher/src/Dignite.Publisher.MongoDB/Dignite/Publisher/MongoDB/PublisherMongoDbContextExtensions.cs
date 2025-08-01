using Dignite.FileExplorer.MongoDB;
using Dignite.Publisher.Categories;
using Dignite.Publisher.Posts;
using MongoDB.Bson.Serialization;
using Volo.Abp;
using Volo.Abp.MongoDB;
using Volo.CmsKit.MongoDB;

namespace Dignite.Publisher.MongoDB;

public static class PublisherMongoDbContextExtensions
{
    public static void ConfigurePublisher(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.ConfigureCmsKit();
        builder.ConfigureFileExplorer();

        builder.Entity<Post>(x =>
        {
            x.CollectionName = PublisherDbProperties.DbTablePrefix + "Posts";
        });

        // 注册抽象基类 Post 的映射（作为 RootClass）
        if (!BsonClassMap.IsClassMapRegistered(typeof(Post)))
        {
            BsonClassMap.RegisterClassMap<Post>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
            });
        }

        // 注册子类 ArticlePost
        if (!BsonClassMap.IsClassMapRegistered(typeof(ArticlePost)))
        {
            BsonClassMap.RegisterClassMap<ArticlePost>(cm =>
            {
                cm.AutoMap();
            });
        }

        // 注册子类 VideoPost
        if (!BsonClassMap.IsClassMapRegistered(typeof(VideoPost)))
        {
            BsonClassMap.RegisterClassMap<VideoPost>(cm =>
            {
                cm.AutoMap();
            });
        }


        builder.Entity<Category>(x =>
        {
            x.CollectionName = PublisherDbProperties.DbTablePrefix + "Categories";
        });
        builder.Entity<PostCategory>(x =>
        {
            x.CollectionName = PublisherDbProperties.DbTablePrefix + "PostCategories";
        });
    }
}
