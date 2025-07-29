using System.Text.Json;

namespace Dignite.Publisher.Posts.Serialization;
public class ArticlePostDeserializer : IPostDeserializer
{
    public string PostTypeName => PostTypeConsts.ArticlePostTypeName;

    public PostDto Deserialize(JsonElement element, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<ArticlePostDto>(element.GetRawText(), options);
    }
}
