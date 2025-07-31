using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dignite.Publisher.Admin.Posts.Serialization;
public class PostAdminDtoConverter : JsonConverter<PostAdminDtoBase>
{
    protected IEnumerable<IPostAdminDtoDeserializer> PostAdminDtoDeserializers { get; }
    public PostAdminDtoConverter(IEnumerable<IPostAdminDtoDeserializer> deserializers)
    {
        PostAdminDtoDeserializers = deserializers;
    }

    public override PostAdminDtoBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var root = jsonDoc.RootElement;
        var postTypePropertyName = nameof(PostAdminDtoBase.PostType);
        postTypePropertyName= char.ToLowerInvariant(postTypePropertyName[0]) + postTypePropertyName.Substring(1);  // Convert to camelCase

        if (!root.TryGetProperty(postTypePropertyName, out var typeProperty))
        {
            throw new JsonException("Missing 'postType' property.");
        }

        var postType = typeProperty.GetString();
        var postTypeHandler = PostAdminDtoDeserializers.FirstOrDefault(handler => handler.PostTypeName == postType);
        if (postTypeHandler == null)
        {
            throw new JsonException($"Unknown post type: {postType}");
        }

        return postTypeHandler.Deserialize(root, options);
    }

    public override void Write(Utf8JsonWriter writer, PostAdminDtoBase value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
