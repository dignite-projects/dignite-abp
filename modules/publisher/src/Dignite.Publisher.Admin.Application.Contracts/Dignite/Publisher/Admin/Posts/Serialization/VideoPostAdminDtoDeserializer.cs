using System.Text.Json;
using Dignite.Publisher.Posts;

namespace Dignite.Publisher.Admin.Posts.Serialization;
public class VideoPostAdminDtoDeserializer : IPostAdminDtoDeserializer
{
    public string PostTypeName => PostTypeConsts.VideoPostTypeName;

    public PostAdminDtoBase Deserialize(JsonElement element, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<VideoPostAdminDto>(element.GetRawText(), options);
    }
}
