using System.Text.Json;
using Dignite.Publisher.Features;
using Dignite.Publisher.GlobalFeatures;
using Dignite.Publisher.Posts;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;

namespace Dignite.Publisher.Admin.Posts.Serialization;

[RequiresGlobalFeature(typeof(ArticlePostsFeature))]
[RequiresFeature(PublisherFeatures.ArticlePostEnable)]
public class ArticlePostInputDeserializer : IPostInputDeserializer
{
    public string PostTypeName => PostTypeConsts.ArticlePostTypeName;
    public CreatePostInput DeserializeForCreate(JsonElement element, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<CreateArticlePostInput>(element.GetRawText(), options)!;
    }
    public UpdatePostInput DeserializeForUpdate(JsonElement element, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<UpdateArticlePostInput>(element.GetRawText(), options)!;
    }
}