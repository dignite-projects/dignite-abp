using System.Text.Json;
using Dignite.Publisher.Posts;

namespace Dignite.Publisher.Admin.Posts.Serialization;
public class ArticlePostAdminDtoDeserializer : IPostAdminDtoDeserializer
{
    public string PostTypeName => PostTypeConsts.ArticlePostTypeName;

    public PostAdminDtoBase Deserialize(JsonElement element, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<ArticlePostAdminDto>(element.GetRawText(), options);
    }
}
