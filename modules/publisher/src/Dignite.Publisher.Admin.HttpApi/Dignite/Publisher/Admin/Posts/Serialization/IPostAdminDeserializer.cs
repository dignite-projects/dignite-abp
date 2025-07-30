using System.Text.Json;
using Volo.Abp.DependencyInjection;

namespace Dignite.Publisher.Admin.Posts.Serialization;

/// <summary>
/// 用于从 JSON 元素反序列化创建和更新的 DTO。
/// 注意：实现类必须以 "PostAdminDeserializer" 结尾，以确保依赖注入机制能正确识别。
/// </summary>
public interface IPostAdminDeserializer : ITransientDependency
{
    /// <summary>
    /// The name of the post type, e.g. "Article", "Video", etc.
    /// </summary>
    string PostTypeName { get; }

    /// <summary>
    /// Deserializes a JSON element into a PostDto.
    /// </summary>
    /// <param name="element"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    CreatePostInput DeserializeForCreate(JsonElement element, JsonSerializerOptions options);

    /// <summary>
    /// Deserializes a JSON element into a PostDto.
    /// </summary>
    /// <param name="element"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    UpdatePostInput DeserializeForUpdate(JsonElement element, JsonSerializerOptions options);
}
