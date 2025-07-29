using System.Text.Json;

namespace Dignite.Publisher.Posts.Serialization;
public class VideoPostDeserializer : IPostDeserializer
{
    public string PostTypeName => PostTypeConsts.VideoPostTypeName;

    public PostDto Deserialize(JsonElement element, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<VideoPostDto>(element.GetRawText(), options);
    }
}
