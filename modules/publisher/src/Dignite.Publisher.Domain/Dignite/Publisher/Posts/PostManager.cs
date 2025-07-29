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

    public virtual async Task DeleteAsync(Guid id)
    {
        await PostRepository.DeleteAsync(id);
    }

    public virtual async Task CheckSlugExistenceAsync(string? local, string slug)
    {
        if (await PostRepository.SlugExistsAsync(local, slug))
        {
            throw new PostSlugAlreadyExistException(local, slug);
        }
    }
}
