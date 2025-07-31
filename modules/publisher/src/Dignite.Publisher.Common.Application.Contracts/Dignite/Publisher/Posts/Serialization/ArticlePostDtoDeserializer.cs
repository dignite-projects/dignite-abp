using System.Text.Json;

namespace Dignite.Publisher.Posts.Serialization;
public class ArticlePostDtoDeserializer : IPostDtoDeserializer
{
    public string PostTypeName => PostTypeConsts.ArticlePostTypeName;

    public PostDtoBase Deserialize(JsonElement element, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<ArticlePostDto>(element.GetRawText(), options);
    }
}
