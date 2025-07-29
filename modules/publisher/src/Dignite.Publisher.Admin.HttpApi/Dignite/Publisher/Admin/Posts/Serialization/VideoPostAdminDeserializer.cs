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
    public CreatePostDto DeserializeForCreate(JsonElement element, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<CreateVideoPostDto>(element.GetRawText(), options)!;
    }
    public UpdatePostDto DeserializeForUpdate(JsonElement element, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<UpdateVideoPostDto>(element.GetRawText(), options)!;
    }
}