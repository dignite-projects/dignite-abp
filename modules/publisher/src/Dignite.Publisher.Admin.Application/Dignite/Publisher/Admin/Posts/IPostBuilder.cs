using System;
using Dignite.Publisher.Posts;
using Volo.Abp.DependencyInjection;

namespace Dignite.Publisher.Admin.Posts;

/// <summary>
/// 用于从创建和更新的 DTO 构建不同类型的 <see cref="Post"/> 实体。
/// 注意：实现类必须以 "PostBuilder" 结尾，以确保依赖注入机制能正确识别。
/// </summary>
public interface IPostBuilder : ITransientDependency
{
    /// <summary>
    /// The name of the post type, e.g. "Article", "Page", etc.
    /// </summary>
    string PostTypeName { get; }

    /// <summary>
    /// Builds a post entity from the provided create DTO.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="postId"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    Post Create(CreatePostInput input, Guid postId, Guid? tenantId);

    /// <summary>
    /// Builds a post entity from the provided update DTO.
    /// </summary>
    /// <param name="post"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    void Update(Post post, UpdatePostInput input);
}
