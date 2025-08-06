using Dignite.FileExplorer;
using Dignite.Publisher.Posts;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Modularity;
using Volo.CmsKit;
using Volo.CmsKit.Comments;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.MarkedItems;
using Volo.CmsKit.Reactions;
using Volo.CmsKit.Tags;

namespace Dignite.Publisher;

[DependsOn(
    typeof(CmsKitDomainModule),
    typeof(FileExplorerDomainModule),
    typeof(PublisherDomainSharedModule)
)]
public class PublisherDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        if (GlobalFeatureManager.Instance.IsEnabled<ReactionsFeature>())
        {
            Configure<CmsKitReactionOptions>(options =>
            {
                options.EntityTypes.Add(
                    new ReactionEntityTypeDefinition(
                        PostConsts.EntityType,
                        reactions: new[]
                        {
                            new ReactionDefinition(StandardReactions.ThumbsUp)
                        }));

                if (GlobalFeatureManager.Instance.IsEnabled<CommentsFeature>())
                {
                    options.EntityTypes.Add(
                        new ReactionEntityTypeDefinition(
                            CommentConsts.EntityType,
                            reactions: new[]
                            {
                                new ReactionDefinition(StandardReactions.ThumbsUp)
                            }));
                }
            });
        }

        if (GlobalFeatureManager.Instance.IsEnabled<CommentsFeature>())
        {
            Configure<CmsKitCommentOptions>(options =>
            {
                options.EntityTypes.Add(new CommentEntityTypeDefinition(PostConsts.EntityType));
            });
        }

        if (GlobalFeatureManager.Instance.IsEnabled<TagsFeature>())
        {
            Configure<CmsKitTagOptions>(options =>
            {
                options.EntityTypes.Add(
                    new TagEntityTypeDefiniton(PostConsts.EntityType));
            });
        }

        if (GlobalFeatureManager.Instance.IsEnabled<MarkedItemsFeature>())
        {
            Configure<CmsKitMarkedItemOptions>(options =>
            {
                options.EntityTypes.Add(
                    new MarkedItemEntityTypeDefinition(
                        PostConsts.EntityType,
                        StandardMarkedItems.Favorite
                        )
                    );
            });
        }
    }
}
