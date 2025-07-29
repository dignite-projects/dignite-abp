using System.Text.Json;
using Volo.Abp.DependencyInjection;

namespace Dignite.Publisher.Posts.Serialization;

/// <summary>
/// 用于从 JSON 元素反序列化 <see cref="PostDto" />。
/// 注意：实现类必须以 "PostDeserializer" 结尾，以确保依赖注入机制能正确识别。
/// </summary>
public interface IPostDeserializer: ITransientDependency
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
    PostDto Deserialize(JsonElement element, JsonSerializerOptions options);
}
