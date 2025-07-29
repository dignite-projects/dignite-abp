using Dignite.Publisher.Categories;
using Dignite.Publisher.GlobalFeatures;
using Dignite.Publisher.Posts;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.GlobalFeatures;
using Volo.CmsKit.EntityFrameworkCore;

namespace Dignite.Publisher.EntityFrameworkCore;

public static class PublisherDbContextModelCreatingExtensions
{
    public static void ConfigurePublisher(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        //
        builder.ConfigureCmsKit();

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(PublisherDbProperties.DbTablePrefix + "Questions", PublisherDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */

        builder.Entity<Category>(b =>
        {
            b.Ignore(c => c.Children); // Ignore the Children property as it is not needed in the database schema.

            //Configure table & schema name
            b.ToTable(PublisherDbProperties.DbTablePrefix + "Categories", PublisherDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.DisplayName).IsRequired().HasMaxLength(CategoryConsts.MaxDisplayNameLength);
            b.Property(q => q.Name).IsRequired().HasMaxLength(CategoryConsts.MaxNameLength);
            b.Property(q => q.Local).HasMaxLength(CategoryConsts.MaxLocalLength);
            b.Property(q => q.Description).HasMaxLength(CategoryConsts.MaxDescriptionLength);

            //Relations
            b.HasMany(p => p.CategoryPosts)
                .WithOne(pc => pc.Category)
                .HasForeignKey(pc => pc.CategoryId)
                .IsRequired();

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<Post>(b =>
        {
            b.Ignore(c => c.PostType); //Ignore the PostType property as it is not needed in the database schema.

            //Configure table & schema name
            b.ToTable(PublisherDbProperties.DbTablePrefix + "Posts", PublisherDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(PostConsts.MaxTitleLength);
            b.Property(q => q.Slug).IsRequired().HasMaxLength(PostConsts.MaxSlugLength);
            b.Property(q => q.CoverImageUrl).HasMaxLength(PostConsts.MaxCoverImageUrlLength);
            b.Property(q => q.Summary).HasMaxLength(PostConsts.MaxSummaryLength);
            b.Property(q => q.Local).HasMaxLength(PostConsts.MaxLocalLength);
            b.Property(p => p.CreationTime).HasColumnType("smalldatetime");
            b.Property(p => p.PublishedTime).HasColumnType("smalldatetime");

            //Discriminator for TPH inheritance
            b.HasDiscriminator();
            b.Property<string>("Discriminator").HasMaxLength(16); // This is the built-in field name that distinguishes PostType in the TPH model.

            //Discriminator values for different post types
            if (GlobalFeatureManager.Instance.IsEnabled<ArticlePostsFeature>())
            {
                b.HasDiscriminator().HasValue<ArticlePost>(PostTypeConsts.ArticlePostTypeName);
            }
            if (GlobalFeatureManager.Instance.IsEnabled<VideoPostsFeature>())
            {
                b.HasDiscriminator().HasValue<VideoPost>(PostTypeConsts.VideoPostTypeName);
            }


            //Relations
            b.HasMany(p => p.PostCategories)
                .WithOne(pc => pc.Post)
                .HasForeignKey(pc => pc.PostId)
                .IsRequired();

            //Indexes
            b.HasIndex(p => new { p.Slug });
            b.HasIndex(p => new { p.CreatorId, p.Status });
            b.HasIndex(p => new { p.Status });

            b.ApplyObjectExtensionMappings();
        });

        if (GlobalFeatureManager.Instance.IsEnabled<ArticlePostsFeature>())
        {
            builder.Entity<ArticlePost>(b =>
            {
                b.Property(p => p.Content).HasColumnName($"{PostTypeConsts.ArticlePostTypeName}_{nameof(ArticlePost.Content)}");
            });
        }
        else
        {
            builder.Ignore<ArticlePost>();
        }

        if (GlobalFeatureManager.Instance.IsEnabled<VideoPostsFeature>())
        {
            builder.Entity<VideoPost>(b =>
            {
                b.Property(p => p.VideoUrl).HasMaxLength(VideoPostConsts.MaxVideoUrlLength).HasColumnName($"{PostTypeConsts.VideoPostTypeName}_{nameof(VideoPost.VideoUrl)}");
                b.Property(p => p.Duration).HasColumnName($"{PostTypeConsts.VideoPostTypeName}_{nameof(VideoPost.Duration)}");
                b.Property(p => p.Description).HasMaxLength(VideoPostConsts.MaxDescriptionLength).HasColumnName($"{PostTypeConsts.VideoPostTypeName}_{nameof(VideoPost.Description)}");
            });
        }
        else
        {
            builder.Ignore<VideoPost>();
        }

        builder.Entity<PostCategory>(b =>
        {
            //Configure table & schema name
            b.ToTable(PublisherDbProperties.DbTablePrefix + "PostCategories", PublisherDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Indexes
            b.HasIndex(pc => pc.CategoryId);
            b.HasIndex(pc => pc.PostId);

            //Key
            b.HasKey(pc => new { pc.CategoryId, pc.PostId });

            //Relations
            b.HasOne(pc => pc.Post)
                .WithMany(p => p.PostCategories)
                .HasForeignKey(pc => pc.PostId);
            b.HasOne(pc => pc.Category)
                .WithMany(c => c.CategoryPosts)
                .HasForeignKey(pc => pc.CategoryId);

            b.ApplyObjectExtensionMappings();
        });
    }
}
