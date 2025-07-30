using System.Text.Json;
using Dignite.Publisher.Features;
using Dignite.Publisher.GlobalFeatures;
using Dignite.Publisher.Posts;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;

namespace Dignite.Publisher.Admin.Posts.Serialization;

[RequiresGlobalFeature(typeof(ArticlePostsFeature))]
[RequiresFeature(PublisherFeatures.ArticlePostEnable)]
public class VideoPostAdminDeserializer : IPostAdminDeserializer
{
    public string PostTypeName => PostTypeConsts.VideoPostTypeName;
    public CreatePostInput DeserializeForCreate(JsonElement element, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<CreateVideoPostInput>(element.GetRawText(), options)!;
    }
    public UpdatePostInput DeserializeForUpdate(JsonElement element, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<UpdateVideoPostInput>(element.GetRawText(), options)!;
    }
}