using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Dignite.Publisher.Posts;
public class PostManager: DomainService
{
    protected IPostRepository PostRepository { get; }

    public PostManager(IPostRepository postRepository)
    {
        PostRepository = postRepository;
    }

    public virtual async Task DeleteAsync(Post post)
    {
        await PostRepository.DeleteAsync(post);
    }

    public virtual async Task CheckSlugExistenceAsync(string? locale, string slug)
    {
        if (await PostRepository.SlugExistsAsync(locale, slug))
        {
            throw new PostSlugAlreadyExistException(locale, slug);
        }
    }
}
