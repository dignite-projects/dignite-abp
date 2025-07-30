using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dignite.Publisher.Admin.Posts.Serialization;
public class CreatePostDtoConverter : JsonConverter<CreatePostInput>
{
    protected IEnumerable<IPostAdminDeserializer> Deserializers { get; }
    public CreatePostDtoConverter(IEnumerable<IPostAdminDeserializer> deserializers)
    {
        Deserializers = deserializers;
    }

    public override CreatePostInput Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var root = jsonDoc.RootElement;
        var postTypePropertyName = nameof(CreateOrUpdatePostInputBase.PostType);
        postTypePropertyName = char.ToLowerInvariant(postTypePropertyName[0]) + postTypePropertyName.Substring(1);  // Convert to camelCase

        if (!root.TryGetProperty(postTypePropertyName, out var typeProperty))
        {
            throw new JsonException("Missing 'postType' property.");
        }

        var postType = typeProperty.GetString();
        var deserializer = Deserializers.FirstOrDefault(x => x.PostTypeName == postType);
        if (deserializer == null)
        {
            throw new JsonException($"Unknown post type: {postType}");
        }

        return deserializer.DeserializeForCreate(root, options);
    }

    public override void Write(Utf8JsonWriter writer, CreatePostInput value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
