using System.Text.Json;

namespace Dignite.Publisher.Posts.Serialization;
public class VideoPostDtoDeserializer : IPostDtoDeserializer
{
    public string PostTypeName => PostTypeConsts.VideoPostTypeName;

    public PostDtoBase Deserialize(JsonElement element, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<VideoPostDto>(element.GetRawText(), options);
    }
}
