using System.Text.Json;
using Dignite.Publisher.Features;
using Dignite.Publisher.GlobalFeatures;
using Dignite.Publisher.Posts;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;

namespace Dignite.Publisher.Admin.Posts.Serialization;

[RequiresGlobalFeature(typeof(ArticlePostsFeature))]
[RequiresFeature(PublisherFeatures.ArticlePostEnable)]
public class ArticlePostAdminDeserializer : IPostAdminDeserializer
{
    public string PostTypeName => PostTypeConsts.ArticlePostTypeName;
    public CreatePostDto DeserializeForCreate(JsonElement element, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<CreateArticlePostDto>(element.GetRawText(), options)!;
    }
    public UpdatePostDto DeserializeForUpdate(JsonElement element, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<UpdateArticlePostDto>(element.GetRawText(), options)!;
    }
}